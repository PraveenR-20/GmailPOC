using Gmail_POC.Application.Interfaces;
using Gmail_POC.Data.Models;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Gmail_POC.Data.Common;
using Gmail_POC.Utility.ConfigurationSettings;
using Microsoft.Extensions.Options;

namespace Gmail_POC.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        #region Private Fields
        private readonly IUserService _userService;
        private readonly ILogger<UsersController> _logger;
        private readonly UserSecretSetting _userSecretSetting;
        private readonly IStringLocalizer<UsersController> _localizer;

        static string[] scopes = { CalendarService.Scope.CalendarEventsReadonly };
        static string applicationName;
        #endregion

        #region Constructor

        /// <summary>
        /// Constructor and param initilization
        /// </summary>
        /// <param name="userService"></param>
        /// <param name="logger"></param>
        /// <param name="userSecretSetting"></param>
        /// <param name="localizer"></param>
        public UsersController(IUserService userService,
            ILogger<UsersController> logger,
            IOptions<UserSecretSetting> userSecretSetting,
            IStringLocalizer<UsersController> localizer)
        {
            _userService = userService;
            _logger = logger;
            _localizer = localizer;
            _userSecretSetting = userSecretSetting.Value;

            applicationName = _localizer.GetString("GmailEventsPOC").Value;
        }
        #endregion

        #region Public Methods              

        #region POST
        /// <summary>
        /// This method is used for OnBoard a new user in system.
        /// </summary>
        /// <param name="user">we need to pass all the required information in user object</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Data.Models.Users user)
        {
            var result = Common.IsValidEmail(user.Email);
            if (!result) return BadRequest(_localizer.GetString("InvalidEmail").Value);
            var isEmailExist = await _userService.IsUserExist(user.Email);
            if (isEmailExist) return BadRequest(_localizer.GetString("EmailExist").Value);
            if (string.IsNullOrWhiteSpace(user.ClientId)) return BadRequest(_localizer.GetString("ErrorClientIdRequired").Value);
            if (string.IsNullOrWhiteSpace(user.SecretKey)) return BadRequest(_localizer.GetString("ErrorSecretKeyRequired").Value);
            user.ClientId = Encryption.EncryptSecrets(user.ClientId, _userSecretSetting.EncryptionKey);
            user.SecretKey = Encryption.EncryptSecrets(user.SecretKey, _userSecretSetting.EncryptionKey);
            await _userService.AddUser(user);
            return Ok(_localizer.GetString("UserCreatedSuccessfully").Value);
        }
        #endregion

        #region GET Users list
        /// <summary>
        /// This will get all list of users.
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Get()
        {

            var users = await _userService.GetUsers();
            foreach (var u in users)
            {
                u.ClientId = Encryption.DecryptSecrets(u.ClientId, _userSecretSetting.EncryptionKey);
                u.SecretKey = Encryption.DecryptSecrets(u.SecretKey, _userSecretSetting.EncryptionKey);
            }
            return Ok(users);
        }
        #endregion

        #region Get event List
        /// <summary>
        /// This method is used for get all the events
        /// </summary>
        /// <returns></returns>
        [HttpGet]       
        [Route("events")]       
        public async Task<IActionResult> GetEvents()
        {

            try
            {
                UserCredential credential;
                ClientSecrets sec = new ClientSecrets();
                sec.ClientId = _userSecretSetting.GoogleClientId;
                sec.ClientSecret = _userSecretSetting.GoogleClientSecret;
                var result = await GoogleWebAuthorizationBroker.AuthorizeAsync(sec, scopes, "user", CancellationToken.None, new FileDataStore(_localizer.GetString("GmailPOC").Value));
                string credPath = _localizer.GetString("TokenJson").Value;
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(sec, scopes, _localizer.GetString("User").Value, CancellationToken.None, new FileDataStore(credPath, true)).Result;
                // Create Google Calendar API service.
                var service = new CalendarService(new BaseClientService.Initializer() { HttpClientInitializer = result, ApplicationName = applicationName });
                var newYearDt = new DateTime(year: 2021, month: 1, day: 1);
                // Define parameters of request.
                EventsResource.ListRequest request = service.Events.List(_localizer.GetString("Primary").Value);
                request.TimeMin = DateTime.Now;
                request.TimeMax = new DateTime(year: 2021, month: 1, day: 1);
                request.MaxResults = 40;
                request.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;
                request.ShowDeleted = false;
                request.PrettyPrint = true;
                request.ShowDeleted = false;
                request.SingleEvents = true;
                Events events = request.Execute();
                await _userService.ManageEvents(events);
                return Ok(events.Items);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Ok(ModelState);
            }
        }
        #endregion

        #endregion

    }
}
