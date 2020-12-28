using AutoMapper;
using Gmail_POC.Application.Interfaces;
using Gmail_POC.Data.Common;
using Gmail_POC.Data.Interfaces;
using Gmail_POC.Data.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gmail_POC.Application.Services
{
    public class UserService : IUserService
    {
        #region Private Fields

        private readonly ILogger<UserService> _logger;
        private readonly IUserRepository _userRepository;

        #endregion

        #region Constrouctor

        public UserService(ILogger<UserService> logger, IUserRepository userRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
        }

        #endregion

        #region public methods   
        /// <summary>
        ///Method Used for all new events
        /// </summary>
        /// <param name="events"></param>
        /// <returns></returns>
        public async Task<bool> AddEvents(UserEvent events)
        {
            try
            {
                await _userRepository.AddUserEvents(events);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        /// <summary>
        /// this method is used for get all the events by calender Id
        /// </summary>
        /// <param name="calId"></param>
        /// <param name="startDateTime"></param>
        /// <param name="endDateTime"></param>
        /// <returns></returns>
        public async Task<UserEvent> GetUserEventByCalId(string calId, DateTime startDateTime, DateTime endDateTime)
        {
            try
            {
                return await _userRepository.GetUserEventByCalId(calId, startDateTime, endDateTime);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        /// <summary>
        /// This method is used for update users event
        /// </summary>
        /// <param name="userEvent"></param>
        /// <returns></returns>
        public async Task<bool> UpdateUserEvent(UserEvent userEvent)
        {
            try
            {
                await _userRepository.UpdateUserEvent(userEvent);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Method used for add event attendees
        /// </summary>
        /// <param name="eventAttendee"></param>
        /// <returns></returns>
        public async Task<bool> AddEventAttendees(List<EventAttendee> eventAttendee)
        {
            return await _userRepository.AddEventAttendees(eventAttendee);
        }



        /// <summary>
        /// Method used for add recurring events
        /// </summary>
        /// <param name="userRecurringEvents"></param>
        /// <returns></returns>
        public async Task<bool> AddRecurringEvents(List<UserRecurringEvent> userRecurringEvents)
        {
            return await _userRepository.AddRecurringEvents(userRecurringEvents);
        }

        /// <summary>
        /// Method used for get event attendee list by user event id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<List<EventAttendee>> GetEventAttendeesByUserEventId(int id)
        {
            return await _userRepository.GetEventAttendeesByUserEventId(id);
        }

        /// <summary>
        /// This method is used for get reccuring event by User event id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<List<UserRecurringEvent>> GetRecurringEventByUserEventId(int id)
        {
            return await _userRepository.GetRecurringEventByUserEventId(id);
        }

        /// <summary>
        /// This is used for get User event by recurring Id
        /// </summary>
        /// <param name="recurringId"></param>
        /// <returns></returns>
        public async Task<UserEvent> GetUserEventByRecurringId(string recurringId)
        {
            return await _userRepository.GetUserEventByRecurringId(recurringId);
        }

        /// <summary>
        /// Method used for delete event attendees
        /// </summary>
        /// <param name="eventAttendees"></param>
        /// <returns></returns>

        public async Task<bool> DeleteEventAttendees(List<EventAttendee> eventAttendees)
        {
            return await _userRepository.DeleteEventAttendees(eventAttendees);
        }

        /// <summary>
        /// method used for delete recurring events
        /// </summary>
        /// <param name="userRecurringEvents"></param>
        /// <returns></returns>

        public async Task<bool> DeleteRecurringEvents(List<UserRecurringEvent> userRecurringEvents)
        {
            return await _userRepository.DeleteRecurringEvents(userRecurringEvents);
        }


        /// <summary>
        /// Method used for get all the event except calender Id
        /// </summary>
        /// <param name="calIds"></param>
        /// <returns></returns>
        public async Task<List<UserEvent>> GetUserEventExceptsCalId(List<string> calIds)
        {
            return await _userRepository.GetUserEventExceptsCalId(calIds);
        }

        /// <summary>
        /// Method used for Delete User events
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteUserEvents(List<int> id)
        {
            return await _userRepository.DeleteUserEvents(id);
        }

        /// <summary>
        /// Method used for Onboard new user in system
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<bool> AddUser(Users user)
        {
            return await _userRepository.AddUser(user);
        }

        /// <summary>
        /// Method check user exist in DB or not
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public async Task<bool> IsUserExist(string email)
        {
            return await _userRepository.IsUserExist(email);
        }

        /// <summary>
        /// This will return all list of users
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Users>> GetUsers()
        {
            return await _userRepository.GetUsers();
        }

        /// <summary>
        /// Method used for get and manage all the events
        /// </summary>
        /// <param name="events"></param>
        /// <returns></returns>
        public async Task ManageEvents(Google.Apis.Calendar.v3.Data.Events events)
        {
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

                    userEvent = await GetUserEventByCalId(firstEvent.ICalUID, startDate.Value, endDate.Value);

                    //inserting new events
                    if (userEvent == null)
                    {
                        var evts = Common.PrepareUserEventDataModel(new UserEvent(), firstEvent);
                        await AddEvents(evts);

                        //inserting new attendees
                        if (firstEvent.Attendees != null)
                        {
                            var attendess = new List<Data.Models.EventAttendee>();
                            foreach (var a in firstEvent.Attendees)
                            {
                                var eventAttendee = Common.PrepareEventAttendeeDataModel(new Data.Models.EventAttendee(), a, evts.Id);
                                attendess.Add(eventAttendee);
                            }
                            await AddEventAttendees(attendess);
                        }

                        //inserting recurring events
                        var recurringEvents = new List<UserRecurringEvent>();
                        foreach (var _u in u)
                        {
                            var userRecurringEvent = Common.PrepareUserRecurringEventDataModel(new UserRecurringEvent(), _u, evts.Id);
                            recurringEvents.Add(userRecurringEvent);
                        }
                        await AddRecurringEvents(recurringEvents);
                    }
                    else
                    {
                        //updating user event
                        userEvent = Common.PrepareUserEventDataModel(userEvent, firstEvent);
                        await UpdateUserEvent(userEvent);

                        //updating event attendees
                        var attendees = await GetEventAttendeesByUserEventId(userEvent.Id);
                        if (firstEvent.Attendees != null)
                        {
                            await DeleteEventAttendees(attendees);
                            var _attendeesList = new List<Data.Models.EventAttendee>();

                            foreach (var a in firstEvent.Attendees)
                            {
                                var _attendees = Common.PrepareEventAttendeeDataModel(new Data.Models.EventAttendee(), a, userEvent.Id);
                                _attendeesList.Add(_attendees);
                            }
                            await AddEventAttendees(_attendeesList);
                        }
                        else await DeleteEventAttendees(attendees);

                        //updating recurring event
                        var recurringEvents = await GetRecurringEventByUserEventId(userEvent.Id);
                        await DeleteRecurringEvents(recurringEvents);

                        var _recurringEvents = new List<UserRecurringEvent>();
                        foreach (var _u in u)
                        {
                            var userRecurringEvent = Common.PrepareUserRecurringEventDataModel(new UserRecurringEvent(), _u, userEvent.Id);
                            _recurringEvents.Add(userRecurringEvent);
                        }
                        await AddRecurringEvents(_recurringEvents);
                    }
                }

                var userEventsDelete = await GetUserEventExceptsCalId(availableKeys);
                if (userEventsDelete != null)
                {
                    await DeleteUserEvents(userEventsDelete.Select(a => a.Id).ToList());
                }

            }
            await Task.CompletedTask;
        }
        #endregion
    }
}
