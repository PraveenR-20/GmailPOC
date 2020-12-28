using Gmail_POC.Data.Context;
using Gmail_POC.Data.Interfaces;
using Gmail_POC.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gmail_POC.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserContext _context;

        public UserRepository(UserContext userContext)
        {
            _context = userContext;
        }

        #region Events
        /// <summary>
        /// Method used for add users event
        /// </summary>
        /// <param name="events"></param>
        /// <returns></returns>
        public async Task<bool> AddUserEvents(UserEvent events)
        {
            await _context.UserEvents.AddAsync(events);
            await _context.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// This method is used for get All the events by calender Id
        /// </summary>
        /// <param name="calId"></param>
        /// <param name="startDateTime"></param>
        /// <param name="endDateTime"></param>
        /// <returns></returns>
        public async Task<UserEvent> GetUserEventByCalId(string calId, DateTime startDateTime, DateTime endDateTime)
        {
            try
            {
                var data = await (from e in _context.UserEvents
                                  join a in _context.UserRecurringEvents on e.Id equals a.UserEventId
                                  where e.ICalUid == calId
                                  orderby a.StartDatatime
                                  select e).FirstOrDefaultAsync();
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// This method is used for update user events
        /// </summary>
        /// <param name="userEvent"></param>
        /// <returns></returns>
        public async Task<bool> UpdateUserEvent(UserEvent userEvent)
        {
            _context.Attach(userEvent).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;

        }

        /// <summary>
        /// Method used for update user data
        /// </summary>
        /// <param name="objUser"></param>
        /// <returns></returns>
        public async Task<UserModel> UpdateUserAsync(UserModel objUser)
        {
            UserModel userResponse = null;
            var user = await _context.User.Where(m => m.Id == objUser.Id).FirstOrDefaultAsync();
            if (user != null)
            {
                user.Name = objUser.Name;
                user.UpdatedOn = DateTime.UtcNow;
                user.UpdatedBy = objUser.UpdatedBy ?? string.Empty;

                var result = await _context.SaveChangesAsync();
                userResponse = user;
            }
            return userResponse;
        }

        /// <summary>
        /// Method used for add event attendees
        /// </summary>
        /// <param name="eventAttendee"></param>
        /// <returns></returns>
        public async Task<bool> AddEventAttendees(List<EventAttendee> eventAttendee)
        {
            await _context.EventAttendees.AddRangeAsync(eventAttendee);
            await _context.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Method used for add users recurring events
        /// </summary>
        /// <param name="userRecurringEvents"></param>
        /// <returns></returns>

        public async Task<bool> AddRecurringEvents(List<UserRecurringEvent> userRecurringEvents)
        {
            await _context.AddRangeAsync(userRecurringEvents);
            await _context.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Method used for Get Event Attendee list By User Event Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<List<EventAttendee>> GetEventAttendeesByUserEventId(int id) => await _context.EventAttendees.Where(a => a.UserEventId == id).ToListAsync();

        /// <summary>
        /// Method used for Get Recurring Events By User Event Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<List<UserRecurringEvent>> GetRecurringEventByUserEventId(int id) => await _context.UserRecurringEvents.Where(a => a.UserEventId == id).ToListAsync();

        /// <summary>
        /// Method used for Get User Event By Recurring Id
        /// </summary>
        /// <param name="recurringId"></param>
        /// <returns></returns>
        public async Task<UserEvent> GetUserEventByRecurringId(string recurringId) => await _context.UserEvents.Where(a => a.ICalUid.Contains(recurringId)).FirstOrDefaultAsync();


        /// <summary>
        /// Method used for delete event attendees
        /// </summary>
        /// <param name="eventAttendees"></param>
        /// <returns></returns>
        public async Task<bool> DeleteEventAttendees(List<EventAttendee> eventAttendees)
        {
            _context.EventAttendees.RemoveRange(eventAttendees);
            await _context.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Method used for delete Recurring events
        /// </summary>
        /// <param name="userRecurringEvents"></param>
        /// <returns></returns>
        public async Task<bool> DeleteRecurringEvents(List<UserRecurringEvent> userRecurringEvents)
        {
            _context.UserRecurringEvents.RemoveRange(userRecurringEvents);
            await _context.SaveChangesAsync();
            await Task.CompletedTask;
            return true;
        }

        /// <summary>
        /// Method used for User event except Calender Id
        /// </summary>
        /// <param name="calIds"></param>
        /// <returns></returns>
        public async Task<List<UserEvent>> GetUserEventExceptsCalId(List<string> calIds) => await _context.UserEvents.Where(a => !calIds.Contains(a.ICalUid)).ToListAsync();

        /// <summary>
        /// Method used for delete user events
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteUserEvents(List<int> id)
        {
            var userEvents = await _context.UserEvents.Where(a => id.Contains(a.Id)).ToListAsync();
            foreach (var ue in userEvents)
            {
                ue.IsDelete = true;
                _context.Entry(ue).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            return true;
        }

        /// <summary>
        /// Method used for onboard/Add new users in system
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<bool> AddUser(Users user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return true;

        }

        /// <summary>
        /// Method check If user exist in system or not
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public async Task<bool> IsUserExist(string email) => await _context.Users.Where(a => a.Email == email).AnyAsync();

        /// <summary>
        /// Method will return list of users
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Users>> GetUsers() => await _context.Users.ToListAsync();

        #endregion
    }
}
