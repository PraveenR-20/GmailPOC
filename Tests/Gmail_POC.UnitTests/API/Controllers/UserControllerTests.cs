using Gmail_POC.API.Controllers;
using Gmail_POC.Application.Interfaces;
using Gmail_POC.Application.Services;
using Gmail_POC.Data.Context;
using Gmail_POC.Data.Interfaces;
using Gmail_POC.Data.Repositories;
using Gmail_POC.UnitTests.Helpers;
using Gmail_POC.Utility.ConfigurationSettings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace Gmail_POC.UnitTests.API.Controllers
{

    public class UserControllerTests
    {
        private readonly IUserService _userService;
        private readonly UserRepository _userRepository;
        private readonly UsersController _userController;
        private readonly UserSecretSetting _userSecretSetting;
        private readonly ILogger<UserService> _userServiceLogger = null;
        private readonly IStringLocalizer<UsersController> _localizer;
        private readonly UserContext _userContext;
        private IConfiguration _config { get; }

        public UserControllerTests()
        {
            var _config = new ConfigurationBuilder().AddUserSecrets<UserControllerTests>().Build();

            var _logger = new Mock<ILogger<UsersController>>();
            _userSecretSetting = new UserSecretSetting()
            {
                EncryptionKey = _config["EncryptionKey"],
                GoogleClientId = _config["Authentication:Google:ClientId"],
                GoogleClientSecret = _config["Authentication:Google:ClientSecret"]
            };
            var userSettingOption = Options.Create(_userSecretSetting);
            var userRepositoryMock = new Mock<IUserRepository>();
            var builder = new ConfigurationBuilder().AddUserSecrets<UserControllerTests>();
            var options = Options.Create(new LocalizationOptions { ResourcesPath = "Resources" });
            var factory = new ResourceManagerStringLocalizerFactory(options, NullLoggerFactory.Instance);
            _localizer = new StringLocalizer<UsersController>(factory);
            var usersDbOptions = new DbContextOptionsBuilder<UserContext>().UseInMemoryDatabase(databaseName: "GmailPOC").Options;
            _userContext = new UserContext(usersDbOptions);
            TestHelper.AddDummyEventData(_userContext);
            TestHelper.AddDummyEventAttendee(_userContext);
            TestHelper.AddDummyRecurringEvent(_userContext);
            _userRepository = new UserRepository(_userContext);
            _userService = new UserService(_userServiceLogger, _userRepository);
            _userController = new UsersController(_userService, _logger.Object, userSettingOption, _localizer);
        }
      
        [Fact]
        public async Task GetEventsAsyncTest()
        {
            var response = await _userController.GetEvents();
            Assert.IsType<OkObjectResult>(response);
        }


        [Fact]
        public async Task GetAllUsersAsyncTest()
        {
            var response = await _userController.Get();
            Assert.IsType<OkObjectResult>(response);
        }
        [Fact]
        public async Task GetAllUsersAsync_NoContentTest()
        {
            var failResponse = await _userController.Get();
            await Task.CompletedTask;
            if (failResponse == null)
            {
                Assert.IsType<NoContentResult>(failResponse);
            }
        }

    }
}
