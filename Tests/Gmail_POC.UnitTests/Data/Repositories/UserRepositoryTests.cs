using Gmail_POC.Data.Context;
using Gmail_POC.Data.Interfaces;
using Gmail_POC.Data.Models;
using Gmail_POC.Data.Repositories;
using Gmail_POC.UnitTests.Helpers;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
namespace Gmail_POC.UnitTests.Data.Repositories
{
    public class UserRepositoryTests
    {
        private readonly UserRepository _userRepository;
        private readonly UserContext _context;

        public UserRepositoryTests()
        {
            var userDbOptions = new DbContextOptionsBuilder<UserContext>()
           .UseInMemoryDatabase(databaseName: "GmailPOC")
           .Options;

            _context = new UserContext(userDbOptions);
            TestHelper.AddDummyEventData(_context);
            TestHelper.AddDummyEventAttendee(_context);
            TestHelper.AddDummyRecurringEvent(_context);
            _userRepository = new UserRepository(_context);
        }

        [Fact]
        public async Task AddEventAttendeesTest()
        {
            var response = await _userRepository.AddEventAttendees(PrepareEventAttendees());
            Assert.True(response);
        }

        [Fact]
        public async Task AddRecurringEventsTest()
        {
            var response = await _userRepository.AddRecurringEvents(PrepareUserRecurringEvents());
            Assert.True(response);
        }
        [Fact]
        public async Task AddUserEventsTest()
        {
            var response = await _userRepository.AddUserEvents(PrepareUser());
            Assert.True(response);
        }

        [Fact]
        public async Task GetEventAttendeesByUserEventIdTest()
        {
            var response = await _userRepository.GetEventAttendeesByUserEventId(103);
            var result = false;
            if (response != null)
            {
                result = true;
            }
            Assert.True(result);
        }
        [Fact]
        public async Task GetRecurringEventByUserEventIdTest()
        {
            var response = await _userRepository.GetRecurringEventByUserEventId(1);
            var result = false;
            if (response != null)
            {
                result = true;
            }
            Assert.True(result);
        }
        [Fact]
        public async Task GetUserEventByCalIdTest()
        {

            var response = await _userRepository.GetUserEventByCalId("6imngggclbl6lqp9sge8dn46dl@google.com", DateTime.Now, DateTime.Now);
            var result = false;
            if (response != null)
            {
                result = true;
            }
            Assert.True(result);
        }
        [Fact]
        public async Task GetUserEventByRecurringIdTest()
        {
            var response = await _userRepository.GetUserEventByRecurringId("6imngggclbl6lqp9sge8dn46dl@google.com");
            var result = false;
            if (response != null)
            {
                result = true;
            }
            Assert.True(result);
        }


        [Fact]
        public async Task UpdateUserEventTest()
        {
            var data = new UserEvent()
            {
                Id = 1,
                AnyoneCanAddSelf = false,
                AttendeesOmitted = false,
                Created = DateTime.Now,
                CreatedRaw = "",
                CreatorSelf = false,
                EndTimeUnspecified = false,
                GuestsCanInviteOthers = false,
                GuestsCanModify = false,
                GuestsCanSeeOtherGuests = false,
                HangoutLink = "https://meet.google.com/wtz-nbmp-bbp",
                HtmlLink = "https://www.google.com/calendar/event?eid=NmltbmdnZ2NsYmw2bHFwOXNnZThkbjQ2ZGxfMjAyMDEyMjhUMDgzMDAwWiByYWFodWxzaHJpdmFzdGF2YTBAbQ",
                ICalUid = "6imngggclbl6lqp9sge8dn46dl@google.com",
                Kind = "calendar#event",
                Location = "Mumbai, Maharashtra, India",
                Locked = false,
                OrgSelf = false,
                PrivateCopy = false,
                Recurrence = null,
                Sequence = 1,
                Status = "confirmed",
                Summary = "Test Event",
                RecurringEventId = "6imngggclbl6lqp9sge8dn46dl"
            };
            var response = await _userRepository.UpdateUserEvent(data);
            Assert.True(response);
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
                    UserEventId = 2
                }
            };
        }

        private List<EventAttendee> PrepareEventAttendees()
        {
            return new List<EventAttendee>() {
                new EventAttendee(){
                    Email = "test@test.com",
                    UserEventId = 1,
                    Comment= "",
                    DisplayName="test",
                    UserId = "test"
                }
            };
        }

        [Fact]
        public async Task UserListTest()
        {
            var response = await _userRepository.GetUsers();
            var result = false;
            if (response != null)
            {
                result = true;
            }
            Assert.True(result);
        }

        [Fact]
        public async Task DeleteEventAttendeesTest()
        {
            var data = new List<EventAttendee>()
            {
                new EventAttendee(){
                    Id = 1,
                    UserEventId = 1,
                    Email = "test01@gmail.com",
                    ResponseStatus = "accepted"
                },
                new EventAttendee(){
                    Id = 2,
                    UserEventId = 1,
                    Email = "test02@gmail.com",
                    ResponseStatus = "accepted"
                }
            };
            var response = await _userRepository.DeleteEventAttendees(data);
            Assert.True(response);
        }
  
        [Fact]
        public async Task DeleteUserEventsTest()
        {
            var response = await _userRepository.DeleteUserEvents(new List<int>() { 103 });
            Assert.True(response);
        }

    }
}
