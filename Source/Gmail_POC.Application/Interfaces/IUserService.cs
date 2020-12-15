using Gmail_POC.Data.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gmail_POC.Application.Interfaces
{
    public interface IUserService
    {      

        #region Events
        Task<bool> AddEvents(UserEvent events);        
        Task<UserEvent> GetUserEventByCalId(string calId, DateTime startDateTime, DateTime endDateTime);
        Task<bool> UpdateUserEvent(UserEvent userEvent);
        Task<bool> AddEventAttendees(List<EventAttendee> eventAttendee);
        Task<bool> AddRecurringEvents(List<UserRecurringEvent> userRecurringEvents);
        Task<List<EventAttendee>> GetEventAttendeesByUserEventId(int id);
        Task<List<UserRecurringEvent>> GetRecurringEventByUserEventId(int id);
        Task<bool> DeleteEventAttendees(List<EventAttendee> eventAttendees);
        Task<bool> DeleteRecurringEvents(List<UserRecurringEvent> userRecurringEvents);
        Task<UserEvent> GetUserEventByRecurringId(string recurringId);
        Task<List<UserEvent>> GetUserEventExceptsCalId(List<string> calIds);
        Task<bool> DeleteUserEvents(List<int> id);
        Task<bool> AddUser(Users user);
        Task<bool> IsUserExist(string email);
        Task<IEnumerable<Users>> GetUsers();

        #endregion
    }
}
