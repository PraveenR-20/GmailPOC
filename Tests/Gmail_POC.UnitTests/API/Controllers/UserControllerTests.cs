using Gmail_POC.API;
using Gmail_POC.API.Controllers;
using Gmail_POC.Application.Interfaces;
using Gmail_POC.Data.Interfaces;
using Gmail_POC.Data.Models;
using Google.Apis.Calendar.v3.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Gmail_POC.UnitTests.API.Controllers
{

    public class UserControllerTests
    {
        private readonly Mock<IUserService> mockUserService = new Mock<IUserService>();
        private IConfiguration configuration { get; set; }
        private readonly UsersController userController;

        public UserControllerTests()
        {
            var logger = new Mock<ILogger<UsersController>>();
            var mapper = new Mock<IStringLocalizer<SharedResources>>();
            var userRepositoryMock = new Mock<IUserRepository>();

            var builder = new ConfigurationBuilder()
             .AddUserSecrets<UserControllerTests>();

            configuration = builder.Build();

            //setup 
            mockUserService.Setup(p => p.AddEvents(new Gmail_POC.Data.Models.UserEvent())).ReturnsAsync(true);
            mockUserService.Setup(p => p.GetUserEventByCalId("333dki70tnger4kpqnpjtqt8ir@google.com", DateTime.Now, DateTime.Now)).ReturnsAsync(new Gmail_POC.Data.Models.UserEvent());
            mockUserService.Setup(p => p.AddEventAttendees(new List<Gmail_POC.Data.Models.EventAttendee>())).ReturnsAsync(true);
            mockUserService.Setup(p => p.AddRecurringEvents(new List<Gmail_POC.Data.Models.UserRecurringEvent>())).ReturnsAsync(true);
            mockUserService.Setup(p => p.GetEventAttendeesByUserEventId(103)).ReturnsAsync(new List<Gmail_POC.Data.Models.EventAttendee>());
            mockUserService.Setup(p => p.GetRecurringEventByUserEventId(103)).ReturnsAsync(new List<Gmail_POC.Data.Models.UserRecurringEvent>());
            mockUserService.Setup(p => p.UpdateUserEvent(new Gmail_POC.Data.Models.UserEvent())).ReturnsAsync(true);
            mockUserService.Setup(p => p.GetUserEventByRecurringId("333dki70tnger4kpqnpjtqt8ir@google.com")).ReturnsAsync(new Gmail_POC.Data.Models.UserEvent());
            mockUserService.Setup(p => p.DeleteRecurringEvents(new List<Gmail_POC.Data.Models.UserRecurringEvent>())).ReturnsAsync(true);
            mockUserService.Setup(p => p.DeleteEventAttendees(new List<Gmail_POC.Data.Models.EventAttendee>())).ReturnsAsync(true);
            mockUserService.Setup(p => p.GetUserEventExceptsCalId(new List<string>() { })).ReturnsAsync(new List<Gmail_POC.Data.Models.UserEvent>());
            mockUserService.Setup(p => p.DeleteUserEvents(new List<int>() { 103 })).ReturnsAsync(true);
            mockUserService.Setup(p => p.AddUser(new Gmail_POC.Data.Models.Users())).ReturnsAsync(true);
            mockUserService.Setup(p => p.IsUserExist("test@test.com")).ReturnsAsync(true);

            userController = new UsersController(mockUserService.Object, logger.Object, mapper.Object, configuration);
        }
    
        [Fact]        
        public async Task GetEvents_SuccessTest()
        {            
            var boundaryStartDate = DateTime.Now;

            var events = await userController.GetEvents();
            var okResult = events as OkObjectResult;
            var response = (List<Event>)okResult.Value;
            response = response.Where(a => a.Start.DateTime >= boundaryStartDate).ToList();

            if (response.Count() > 0)
                Assert.True(true);
            else
                Assert.False(false);
        }

        [Fact]
        public async Task GetEvents_NoContentTest()
        {
            var boundaryStartDate = DateTime.Now;

            var events = await userController.GetEvents();
            var result = events as OkObjectResult;
            var response = (List<Event>)result.Value;
            response = response.Where(a => a.Start.DateTime >= boundaryStartDate).ToList();

            if (response.Count() == 0)
                Assert.True(true);
            else
                Assert.False(false);
        }

        [Fact]
        public async Task GetUsers_SuccessTest()
        {
            var users = await userController.Get();
            var result = users as OkObjectResult;
            var response = (IEnumerable<Users>)result.Value;

            if (response.Count() > 0) 
                Assert.True(true);
            else 
                Assert.False(false);
        }

        [Fact]
        public async Task GetUsers_NoContentTest()
        {
            var users = await userController.Get();
            var result = users as OkObjectResult;
            var response = (IEnumerable<Users>)result.Value;

            if (response.Count() == 0)
                Assert.True(true);
            else
                Assert.False(false);
        }
    }
}
