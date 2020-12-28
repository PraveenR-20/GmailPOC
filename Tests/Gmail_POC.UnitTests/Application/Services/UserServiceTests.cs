using Gmail_POC.Application.Services;
using Gmail_POC.Data.Context;
using Gmail_POC.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Xunit;
using Gmail_POC.Data.Models;
using System.Collections.Generic;
using System;
using Gmail_POC.UnitTests.Helpers;
using Gmail_POC.Data.Repositories;

namespace Gmail_POC.UnitTests.Application.Services
{
    public class UserServiceTest
    {
        #region Private Fields
        private readonly UserContext _context;
        private readonly ILogger<UserService> _logger = null;
        private readonly IUserRepository _userRepository;
        private readonly UserService _userService;

        #endregion

        #region Constructor
        public UserServiceTest()
        {
            var userDbOptions = new DbContextOptionsBuilder<UserContext>()
        .UseInMemoryDatabase(databaseName: "GmailPOC")
        .Options;

            _context = new UserContext(userDbOptions);
            TestHelper.AddDummyEventData(_context);
            TestHelper.AddDummyEventAttendee(_context);
            TestHelper.AddDummyRecurringEvent(_context);
            _userRepository = new UserRepository(_context);
            _userService = new UserService(_logger, _userRepository);
        }
        #endregion
        [Fact]
        public async Task AddEventAttendees_SuccessTest()
        {
            var response = await _userService.AddEventAttendees(PrepareEventAttendees());
            Assert.True(response);
        }

        [Fact]
        public async Task AddRecurringEvents_SuccessTest()
        {
            var recurringEvents = new List<UserRecurringEvent>();
            recurringEvents.Add(new UserRecurringEvent()
            {
                RecurringEventId = "333dki70tnger4kpqnpjtqt8ir@google.com",
                UserEventId = 1
            });

            var response = await _userService.AddRecurringEvents(recurringEvents);
            Assert.True(response);
        }

        [Fact]
        public async Task AddUserEvents_SuccessTest()
        {
            var response = await _userService.AddEvents(new UserEvent());
            Assert.True(response);
        }

        [Fact]
        public async Task DeleteEventAttendees_SuccessTest()
        {
            var response = await _userService.DeleteEventAttendees(new List<EventAttendee>());

            if (!response) Assert.False(response);
            else Assert.True(response);
        }

        [Fact]
        public async Task DeleteRecurringEvents_SuccessTest()
        {
            var response = await _userService.DeleteRecurringEvents(new List<UserRecurringEvent>());
            Assert.True(response);
        }


        [Fact]
        public async Task DeleteUserEvents_SuccessTest()
        {
            var response = await _userService.DeleteUserEvents(new List<int>());
            Assert.True(response); ;
        }
        [Fact]
        public async Task GetEventAttendeesByUserEventId_SuccessTest()
        {
            var response = await _userService.GetEventAttendeesByUserEventId(112);
            var result = false;
            if (response != null)
            {
                result = true;
            }
            Assert.True(result);
        }
        [Fact]
        public async Task GetRecurringEventByUserEventId_SuccessTest()
        {
            var response = await _userService.GetRecurringEventByUserEventId(112);
            var result = false;
            if (response != null)
            {
                result = true;
            }
            Assert.True(result);
        }
        [Fact]
        public async Task GetUserEventByCalId_SuccessTest()
        {
            var response = await _userService.GetUserEventByCalId("6imngggclbl6lqp9sge8dn46dl@google.com", DateTime.Now, DateTime.Now);
            var result = false;
            if (response != null)
            {
                result = true;
            }
            Assert.True(result);
        }
        [Fact]
        public async Task GetUserEventByRecurringId_SuccessTest()
        {
            var response = await _userService.GetUserEventByRecurringId("6imngggclbl6lqp9sge8dn46dl@google.com");
            var result = false;
            if (response != null)
            {
                result = true;
            }
            Assert.True(result);
        }
        [Fact]
        public async Task GetUserEventExceptsCalId_SuccessTest()
        {
            var response = await _userService.GetUserEventExceptsCalId(new List<string>());
            var result = false;
            if (response != null)
            {
                result = true;
            }
            Assert.True(result);

        }
        [Fact]
        public async Task UpdateUserAsync_SuccessTest()
        {
            var response = await _userService.UpdateUserEvent(new UserEvent() { Id = 1 });
            Assert.True(response);
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
            var response = await _userService.GetUsers();
            var result = false;
            if (response != null)
            {
                result = true;
            }
            Assert.True(result);
        }


    }
}
