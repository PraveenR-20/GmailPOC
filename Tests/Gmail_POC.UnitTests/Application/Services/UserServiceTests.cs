using AutoMapper;
using Gmail_POC.Application.Services;
using Gmail_POC.Data.Context;
using Gmail_POC.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Gmail_POC.Data.Models;
using System.Collections.Generic;
using System;
using Moq;

namespace Gmail_POC.UnitTests.Application.Services
{
    public class UserServiceTest
    {
        #region Private Fields
        private readonly ILogger<UserService> _logger;
        private readonly IMapper _mapper;
        private readonly Mock<IUserRepository> userServiceMock;
        #endregion

        #region Constructor
        public UserServiceTest()
        {
            userServiceMock = new Mock<IUserRepository>();                      

            var userDBoptions = new DbContextOptionsBuilder<UserContext>()
            .UseInMemoryDatabase(databaseName: "UserDatabase")
            .Options;
        }
        #endregion
        [Fact]
        public async Task AddEventAttendees_SuccessTest()
        {
            userServiceMock.Setup(a => a.AddEventAttendees(new List<EventAttendee>()))
                .Returns(Task.FromResult(true));

            var userService = new UserService(_logger, userServiceMock.Object, _mapper);

            var response = await userService.AddEventAttendees(PrepareEventAttendees());

            if (!response) Assert.False(response);
            else Assert.True(response);
        }
        [Fact]
        public async Task AddRecurringEvents_SuccessTest()
        {
            userServiceMock.Setup(a => a.AddRecurringEvents(new List<UserRecurringEvent>()))
              .Returns(Task.FromResult(true));

            var userService = new UserService(_logger, userServiceMock.Object, _mapper);

            var response = await userService.AddRecurringEvents(PrepareUserRecurringEvents());

            if (!response) Assert.False(response);
            else Assert.True(response);

        }
        [Fact]
        public async Task AddUserEvents_SuccessTest()
        {
            userServiceMock.Setup(a => a.AddUserEvents(new UserEvent()))
              .Returns(Task.FromResult(true));

            var userService = new UserService(_logger, userServiceMock.Object, _mapper);

            var response = await userService.AddEvents(new UserEvent());

            if (!response) Assert.False(response);
            else Assert.True(response);


        }
        [Fact]
        public async Task DeleteEventAttendees_SuccessTest()
        {
            userServiceMock.Setup(a => a.DeleteEventAttendees(new List<EventAttendee>()))
               .Returns(Task.FromResult(true));

            var userService = new UserService(_logger, userServiceMock.Object, _mapper);

            var response = await userService.DeleteEventAttendees(new List<EventAttendee>());

            if (!response) Assert.False(response);
            else Assert.True(response);
        }

        [Fact]
        public async Task DeleteRecurringEvents_SuccessTest()
        {
            userServiceMock.Setup(a => a.DeleteRecurringEvents(new List<UserRecurringEvent>()))
               .Returns(Task.FromResult(true));

            var userService = new UserService(_logger, userServiceMock.Object, _mapper);

            var response = await userService.DeleteRecurringEvents(new List<UserRecurringEvent>());

            if (!response) Assert.False(response);
            else Assert.True(response);
        }


        [Fact]
        public async Task DeleteUserEvents_SuccessTest()
        {
            userServiceMock.Setup(a => a.DeleteUserEvents(new List<int>()))
                .Returns(Task.FromResult(true));

            var userService = new UserService(_logger, userServiceMock.Object, _mapper);

            var response = await userService.DeleteUserEvents(new List<int>());

            if (!response) Assert.False(response);
            else Assert.True(response);
        }
        [Fact]
        public async Task GetEventAttendeesByUserEventId_SuccessTest()
        {
            userServiceMock.Setup(a => a.GetEventAttendeesByUserEventId(112))
                 .Returns(Task.FromResult(new List<EventAttendee>()));

            var userService = new UserService(_logger, userServiceMock.Object, _mapper);

            var response = await userService.GetEventAttendeesByUserEventId(112);

            if (response.Count() == 0) Assert.False(false);
            else Assert.True(true);

        }
        [Fact]
        public async Task GetRecurringEventByUserEventId_SuccessTest()
        {
            userServiceMock.Setup(a => a.GetRecurringEventByUserEventId(112))
                  .Returns(Task.FromResult(new List<UserRecurringEvent>()));

            var userService = new UserService(_logger, userServiceMock.Object, _mapper);

            var response = await userService.GetRecurringEventByUserEventId(112);

            if (response.Count() == 0) Assert.False(false);
            else Assert.True(true);
        }
        [Fact]
        public async Task GetUserEventByCalId_SuccessTest()
        {
            userServiceMock.Setup(a => a.GetUserEventByCalId("333dki70tnger4kpqnpjtqt8ir@google.com", DateTime.Now, DateTime.Now))
                 .Returns(Task.FromResult(new UserEvent()));

            var userService = new UserService(_logger, userServiceMock.Object, _mapper);

            var response = await userService.GetUserEventByCalId("333dki70tnger4kpqnpjtqt8ir@google.com", DateTime.Now, DateTime.Now);

            if (response == null) Assert.False(false);
            else Assert.True(true);
        }
        [Fact]
        public async Task GetUserEventByRecurringId_SuccessTest()
        {
            userServiceMock.Setup(a => a.GetUserEventByRecurringId("333dki70tnger4kpqnpjtqt8ir@google.com"))
                    .Returns(Task.FromResult(new UserEvent()));

            var userService = new UserService(_logger, userServiceMock.Object, _mapper);

            var response = await userService.GetUserEventByRecurringId("333dki70tnger4kpqnpjtqt8ir@google.com");

            if (response == null) Assert.False(false);
            else Assert.True(true);


        }
        [Fact]
        public async Task GetUserEventExceptsCalId_SuccessTest()
        {

            userServiceMock.Setup(a => a.GetUserEventExceptsCalId(new List<string>()))
                 .Returns(Task.FromResult(new List<UserEvent>()));

            var userService = new UserService(_logger, userServiceMock.Object, _mapper);

            var response = await userService.GetUserEventExceptsCalId(new List<string>());

            if (response.Count() == 0) Assert.False(false);
            else Assert.True(true);

        }
        [Fact]
        public async Task UpdateUserAsync_SuccessTest()
        {
            userServiceMock.Setup(a => a.UpdateUserEvent(new UserEvent() { Id = 112 }))
                 .Returns(Task.FromResult(true));

            var userService = new UserService(_logger, userServiceMock.Object, _mapper);

            var response = await userService.UpdateUserEvent(new UserEvent() { Id = 112 });

            if (!response) Assert.False(false);
            else Assert.True(true);
        }
        [Fact]
        public async Task UpdateUserEvent_SuccessTest()
        {
            userServiceMock.Setup(a => a.UpdateUserEvent(new UserEvent() { Id = 112 }))
                 .Returns(Task.FromResult(true));

            var userService = new UserService(_logger, userServiceMock.Object, _mapper);

            var response = await userService.UpdateUserEvent(new UserEvent() { Id = 112 });

            if (!response) Assert.False(response);
            else Assert.True(response);
        }


        public List<UserEvent> PreparetUserEvents()
        {
            return new List<UserEvent>()
            {
                new UserEvent(){
                   Id =  1,
                   AnyoneCanAddSelf = true,
                   AttendeesOmitted = true,
                   ConferenceData = "",
                   ICalUid = "333dki70tnger4kpqnpjtqt8ir@google.com",
                   RecurringEventId = "333dki70tnger4kpqnpjtqt8ir@google.com"
                },
                new UserEvent(){
                   Id =  2,
                   AnyoneCanAddSelf = true,
                   AttendeesOmitted = true,
                   ConferenceData = "",
                   ICalUid = "333dki70tnger4kpqnpjtqt8ir@google.com",
                   RecurringEventId = "333dki70tnger4kpqnpjtqt8ir@google.com"
                }
            };
        }

        private UserEvent PrepareUser()
        {
            return new UserEvent()
            {
                Id = 2,
                AnyoneCanAddSelf = true,
                AttendeesOmitted = true,
                ConferenceData = "",
                ICalUid = "333dki70tnger4kpqnpjtqt8ir@google.com",
                RecurringEventId = "333dki70tnger4kpqnpjtqt8ir@google.com"
            };
        }

        private List<UserRecurringEvent> PrepareUserRecurringEvents()
        {
            return new List<UserRecurringEvent>()
            {
                new UserRecurringEvent(){
                    RecurringEventId = "333dki70tnger4kpqnpjtqt8ir@google.com",
                    Id = 1000,
                    UserEventId = 1000
                }
            };
        }

        private List<EventAttendee> PrepareEventAttendees()
        {
            return new List<EventAttendee>() {
                new EventAttendee(){
                    Id = 1000,
                    Email = "test@test.com",
                    UserEventId = 1000,
                    Comment= "",
                    DisplayName="test",
                    UserId = "test"
                }
            };
        }

        [Fact]
        public async Task GetUsers_SuccessTest()
        {
            userServiceMock.Setup(a => a.GetUsers()).Returns(Task.FromResult(PreapareUsersList()));

            var userService = new UserService(_logger, userServiceMock.Object, _mapper);
            var response = await userService.GetUsers();
            if (response == null) Assert.False(false);
            else Assert.True(true);
        }

        private IEnumerable<Users> PreapareUsersList()
        {
            return new List<Users>() {
                 new Users(){
                     ClientId = "test", SecretKey = "test", Email ="test@test.com" , FirstName="test",LastName = "test", Id = 1
                 }

             };
        }

        private bool PrepareResult()
        {
            return true;
        }
    }
}
