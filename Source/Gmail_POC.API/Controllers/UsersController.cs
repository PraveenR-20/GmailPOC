using Gmail_POC.Application.Interfaces;
using Gmail_POC.Data.Models;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Gmail_POC.Data.Common;

namespace Gmail_POC.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        #region Private Fields
        private readonly IUserService _userService;
        private readonly ILogger<UsersController> _logger;
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly IConfiguration _config;
        // If modifying these scopes, delete your previously saved credentials
        // at ~/.credentials/calendar-dotnet-quickstart.json
        static string[] scopes = { CalendarService.Scope.CalendarEventsReadonly };
        static string applicationName = "Gmail Events POC";
        #endregion

        #region Constructor
        public UsersController(IUserService userService,
            ILogger<UsersController> logger,
            IStringLocalizer<SharedResources> localizer,
            IConfiguration config)
        {
            _userService = userService;
            _logger = logger;
            _localizer = localizer;
            _config = config;
        }
        #endregion

        #region Public Methods       

        [HttpGet]
        [Route("Events")]
        public async Task<IActionResult> GetEvents()
        {
            try
            {
                UserCredential credential;
                ClientSecrets sec = new ClientSecrets();

                sec.ClientId = _config["Authentication:Google:ClientId"];
                sec.ClientSecret = _config["Authentication:Google:ClientSecret"];

                var result = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                 sec,
                 scopes,
                 "user",
                 CancellationToken.None,
                 new FileDataStore("Books.ListMyLibrary"));
                
                string credPath = "token.json";
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    sec,
                    scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;

                // Create Google Calendar API service.
                var service = new CalendarService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = result,
                    ApplicationName = applicationName,
                });
                var newYearDt = new DateTime(year: 2021, month: 1, day: 1);
                // Define parameters of request.
                EventsResource.ListRequest request = service.Events.List("primary");
                request.TimeMin = DateTime.Now;
                request.TimeMax = new DateTime(year: 2021, month: 1, day: 1);
                request.MaxResults = 40;
                request.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;
                request.ShowDeleted = false;
                request.PrettyPrint = true;
                request.ShowDeleted = false;
                request.SingleEvents = true;
                Events events = request.Execute();

                var availableKeys = new List<string>();
                if (events.Items != null && events.Items.Count > 0)
                {
                    var eventsGroupByCalId = events.Items.GroupBy(a => a.ICalUID).ToList();
                    var keys = eventsGroupByCalId.Select(a => a.Key).ToList();
                    availableKeys.AddRange(keys);

                    foreach (var u in eventsGroupByCalId)
                    {
                        var firstEvent = u.FirstOrDefault();
                        var startDate = firstEvent.Start.DateTime;
                        var endDate = firstEvent.End.DateTime;

                        var userEvent = new UserEvent();
                        if (startDate == null)
                            if (firstEvent.Start.Date != null)
                            {
                                startDate = DateTime.Parse(firstEvent.Start.Date);
                            }

                        if (endDate == null)
                            if (firstEvent.End.Date != null)
                            {
                                endDate = DateTime.Parse(firstEvent.End.Date);
                            }

                        userEvent = await _userService.GetUserEventByCalId(firstEvent.ICalUID, startDate.Value, endDate.Value);

                        //inserting new events
                        if (userEvent == null)
                        {
                            var evts = Common.PrepareUserEventDataModel(new UserEvent(), firstEvent);
                            await _userService.AddEvents(evts);

                            //inserting new attendees
                            if (firstEvent.Attendees != null)
                            {
                                var attendess = new List<Data.Models.EventAttendee>();
                                foreach (var a in firstEvent.Attendees)
                                {
                                    var eventAttendee = Common.PrepareEventAttendeeDataModel(new Data.Models.EventAttendee(), a, evts.Id);
                                    attendess.Add(eventAttendee);
                                }
                                await _userService.AddEventAttendees(attendess);
                            }

                            //inserting recurring events
                            var recurringEvents = new List<UserRecurringEvent>();
                            foreach (var _u in u)
                            {
                                var userRecurringEvent = Common.PrepareUserRecurringEventDataModel(new UserRecurringEvent(), _u, evts.Id);
                                recurringEvents.Add(userRecurringEvent);
                            }
                            await _userService.AddRecurringEvents(recurringEvents);
                        }
                        else
                        {
                            //updating user event
                            userEvent = Common.PrepareUserEventDataModel(userEvent, firstEvent);
                            await _userService.UpdateUserEvent(userEvent);

                            //updating event attendees
                            var attendees = await _userService.GetEventAttendeesByUserEventId(userEvent.Id);
                            if (firstEvent.Attendees != null)
                            {
                                await _userService.DeleteEventAttendees(attendees);
                                var _attendeesList = new List<Data.Models.EventAttendee>();

                                foreach (var a in firstEvent.Attendees)
                                {
                                    var _attendees = Common.PrepareEventAttendeeDataModel(new Data.Models.EventAttendee(), a, userEvent.Id);
                                    _attendeesList.Add(_attendees);
                                }
                                await _userService.AddEventAttendees(_attendeesList);
                            }
                            else await _userService.DeleteEventAttendees(attendees);

                            //updating recurring event
                            var recurringEvents = await _userService.GetRecurringEventByUserEventId(userEvent.Id);
                            await _userService.DeleteRecurringEvents(recurringEvents);

                            var _recurringEvents = new List<UserRecurringEvent>();
                            foreach (var _u in u)
                            {
                                var userRecurringEvent = Common.PrepareUserRecurringEventDataModel(new UserRecurringEvent(), _u, userEvent.Id);
                                _recurringEvents.Add(userRecurringEvent);
                            }
                            await _userService.AddRecurringEvents(_recurringEvents);
                        }
                    }

                    var userEventsDelete = await _userService.GetUserEventExceptsCalId(availableKeys);
                    if (userEventsDelete != null)
                    {
                        await _userService.DeleteUserEvents(userEventsDelete.Select(a => a.Id).ToList());
                    }

                    return Ok(events.Items);
                }
                else return Ok("No upcoming events found.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ModelState);
            }
        }
        #endregion

        #region POST
        [HttpPost]
        [Route("User")]
        public async Task<IActionResult> Post([FromBody] Data.Models.Users user)
        {
            var result = Common.IsValidEmail(user.Email);
            if (!result)
            {
                return BadRequest("Not a valid email address");
            }

            var isEmailExist = await _userService.IsUserExist(user.Email);
            if (isEmailExist)
            {
                return BadRequest("Email already exist, please try with different email address.");
            }

            await _userService.AddUser(user);
            return Ok("User created successfully.");
        }
        #endregion

        #region Get Events by CalendarId

        [Route("UserEventsByCalendarId")]
        [HttpGet]
        public async Task<IActionResult> GetUserEventsByCalendarId([FromQuery] string calendarId)
        {
            UserCredential credential;
            ClientSecrets sec = new ClientSecrets();

            sec.ClientId = _config["Authentication:Google:ClientId"];
            sec.ClientSecret = _config["Authentication:Google:ClientSecret"];

            var result = await GoogleWebAuthorizationBroker.AuthorizeAsync(
             sec,
             scopes,
             "user",
             CancellationToken.None,
             new FileDataStore("Books.ListMyLibrary"));
           
            string credPath = "token.json";
            credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                sec,
                scopes,
                "user",
                CancellationToken.None,
                new FileDataStore(credPath, true)).Result;

            // Create Google Calendar API service.
            var service = new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = result,
                ApplicationName = applicationName,
            });
            var newYearDt = new DateTime(year: 2021, month: 1, day: 1);
            // Define parameters of request.
            EventsResource.ListRequest request = service.Events.List("primary");
            request.TimeMin = DateTime.Now;
            request.TimeMax = new DateTime(year: 2021, month: 1, day: 1);
            request.ShowDeleted = false;
            request.SingleEvents = true;
            request.MaxResults = 40;
            request.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;
            request.ShowDeleted = false;
            request.ICalUID = calendarId;
            request.PrettyPrint = true;
            Events events = request.Execute();
            return Ok(events);
        }
        #endregion

    }
}
