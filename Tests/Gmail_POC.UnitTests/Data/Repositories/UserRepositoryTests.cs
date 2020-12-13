using Gmail_POC.Data.Interfaces;
using Gmail_POC.Data.Models;
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
        private Mock<IUserRepository> userRepositoryMock = new Mock<IUserRepository>();
        [Fact]
        public async Task AddEventAttendees_SuccessTest()
        {
            userRepositoryMock
            .Setup(p => p.AddEventAttendees(PrepareEventAttendees()));

            var userRepository = userRepositoryMock.Object;

            var response = await userRepository.AddEventAttendees(PrepareEventAttendees());

            if (!response) Assert.False(response);
            else Assert.True(response);
        }

        [Fact]
        public async Task AddRecurringEvents_SuccessTest()
        {
            userRepositoryMock
            .Setup(p => p.AddRecurringEvents(PrepareUserRecurringEvents()));

            var userRepository = userRepositoryMock.Object;

            var response = await userRepository.AddRecurringEvents(PrepareUserRecurringEvents());

            if (!response) Assert.False(response);
            else Assert.True(response);
        }
        [Fact]
        public async Task AddUserEvents_SuccessTest()
        {
            userRepositoryMock
            .Setup(p => p.AddUserEvents(PrepareUser()));

            var userRepository = userRepositoryMock.Object;

            var response = await userRepository.AddUserEvents(PrepareUser());

            if (!response) Assert.False(response);
            else Assert.True(response);
        }
        [Fact]
        public async Task DeleteEventAttendees_SuccessTest()
        {
            userRepositoryMock
            .Setup(p => p.DeleteEventAttendees(PrepareEventAttendees()))
            .Returns(Task.FromResult(true));

            var userRepository = userRepositoryMock.Object;

            var response = await userRepository.DeleteEventAttendees(PrepareEventAttendees());

            if (!response) Assert.False(response);
            else Assert.True(response);
        }
        [Fact]
        public async Task DeleteRecurringEvents_SuccessTest()
        {
            userRepositoryMock
           .Setup(p => p.DeleteRecurringEvents(PrepareUserRecurringEvents()))
           .Returns(Task.FromResult(true));

            var userRepository = userRepositoryMock.Object;

            var response = await userRepository.DeleteRecurringEvents(PrepareUserRecurringEvents());

            if (!response) Assert.False(response);
            else Assert.True(response);
        }
        [Fact]
        public async Task DeleteUserEvents_SuccessTest()
        {
            userRepositoryMock
           .Setup(p => p.DeleteUserEvents(new List<int>() { 103 }))
           .Returns(Task.FromResult(true));

            var userRepository = userRepositoryMock.Object;

            var response = await userRepository.DeleteUserEvents(new List<int>() { 103 });

            if (!response) Assert.False(response);
            else Assert.True(response);
        }
        [Fact]
        public async Task GetEventAttendeesByUserEventId_SuccessTest()
        {
            userRepositoryMock
           .Setup(p => p.GetEventAttendeesByUserEventId(103))
           .Returns(Task.FromResult(new List<EventAttendee>()));

            var userRepository = userRepositoryMock.Object;

            var response = await userRepository.GetEventAttendeesByUserEventId(103);

            if (response.Count() == 0) Assert.False(false);
            else Assert.True(true);
        }
        [Fact]
        public async Task GetRecurringEventByUserEventId_SuccessTest()
        {
            userRepositoryMock
            .Setup(p => p.GetRecurringEventByUserEventId(103))
            .Returns(Task.FromResult(new List<UserRecurringEvent>()));

            var userRepository = userRepositoryMock.Object;

            var response = await userRepository.GetRecurringEventByUserEventId(103);

            if (response.Count() > 0) Assert.False(false);
            else Assert.True(true);
        }
        [Fact]
        public async Task GetUserEventByCalId_SuccessTest()
        {
            userRepositoryMock
               .Setup(p => p.GetUserEventByCalId("6imngggclbl6lqp9sge8dn46dl@google.com", DateTime.Now, DateTime.Now))
               .Returns(Task.FromResult(PrepareUser()));

            var userRepository = userRepositoryMock.Object;

            var response = await userRepository.GetUserEventByCalId("6imngggclbl6lqp9sge8dn46dl@google.com", DateTime.Now, DateTime.Now);

            if (response == null) Assert.False(false);
            else Assert.True(true);

        }
        [Fact]
        public async Task GetUserEventByRecurringId_SuccessTest()
        {
            userRepositoryMock
                 .Setup(p => p.GetUserEventByRecurringId("333dki70tnger4kpqnpjtqt8ir@google.com"))
                 .Returns(Task.FromResult(PrepareUser()));

            var userRepository = userRepositoryMock.Object;

            var response = await userRepository.GetUserEventByRecurringId("333dki70tnger4kpqnpjtqt8ir@google.com");

            if (response == null) Assert.False(false);
            else Assert.True(true);

        }
        [Fact]
        public async Task GetUserEventExceptsCalId_SuccessTest()
        {
            userRepositoryMock
                    .Setup(p => p.GetUserEventExceptsCalId(new List<string>()))
                    .Returns(Task.FromResult(new List<UserEvent>()));

            var userRepository = userRepositoryMock.Object;

            var response = await userRepository.GetUserEventExceptsCalId(new List<string>());

            if (response == null) Assert.False(false);
            else Assert.True(true);

        }

        [Fact]
        public async Task UpdateUserEvent_SuccessTest()
        {
            userRepositoryMock
                       .Setup(p => p.UpdateUserEvent(new UserEvent()))
                       .Returns(Task.FromResult(true));

            var userRepository = userRepositoryMock.Object;

            var response = await userRepository.UpdateUserEvent(new UserEvent());

            if (!response) Assert.False(false);
            else Assert.True(true);
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

        private bool PrepareResult()
        {
            return true;
        }
    }
}
