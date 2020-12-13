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
        private readonly UserContext _userContext;

        public UserRepository(UserContext userContext)
        {
            _userContext = userContext;
        }
       
        #region Events
        public async Task<bool> AddUserEvents(UserEvent events)
        {
            using (var db = new UserContext())
            {
                await db.UserEvents.AddAsync(events);
                await db.SaveChangesAsync();
                return true;
            }
        }
        public async Task<UserEvent> GetUserEventByCalId(string calId, DateTime startDateTime, DateTime endDateTime)
        {
            using (var db = new UserContext())
            {               
                return await (from e in db.UserEvents
                              join a in db.UserRecurringEvents on e.Id equals a.UserEventId
                              where  e.ICalUid == calId
                              orderby a.StartDatatime
                              select e).FirstOrDefaultAsync();
            }
        }

        public async Task<bool> UpdateUserEvent(UserEvent userEvent)
        {
            using (var db = new UserContext())
            {
                db.Attach(userEvent).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return true;
            }
        }

        public async Task<UserModel> UpdateUserAsync(UserModel objUser)
        {
            UserModel userResponse = null;

            var user = await _userContext.User.Where(m => m.Id == objUser.Id).FirstOrDefaultAsync();
            if (user != null)
            {
                user.Name = objUser.Name;
                user.UpdatedOn = DateTime.UtcNow;
                user.UpdatedBy = objUser.UpdatedBy ?? string.Empty;

                var result = await _userContext.SaveChangesAsync();
                userResponse = user;
            }

            return userResponse;
        }

        public async Task<bool> AddEventAttendees(List<EventAttendee> eventAttendee)
        {
            using (var db = new UserContext())
            {
                await db.EventAttendees.AddRangeAsync(eventAttendee);
                await db.SaveChangesAsync();
                return true;
            }
        }

        public async Task<bool> AddRecurringEvents(List<UserRecurringEvent> userRecurringEvents)
        {
            using (var db = new UserContext())
            {
                await db.AddRangeAsync(userRecurringEvents);
                await db.SaveChangesAsync();
                return true;
            }
        }

        public async Task<List<EventAttendee>> GetEventAttendeesByUserEventId(int id)
        {
            using (var db = new UserContext())
            {
                return await db.EventAttendees.Where(a => a.UserEventId == id).ToListAsync();
            }
        }

        public async Task<List<UserRecurringEvent>> GetRecurringEventByUserEventId(int id)
        {
            using (var db = new UserContext())
            {
                return await db.UserRecurringEvents.Where(a => a.UserEventId == id).ToListAsync();
            }
        }

        public async Task<UserEvent> GetUserEventByRecurringId(string recurringId)
        {
            using (var db = new UserContext())
            {
                return await db.UserEvents.Where(a => a.ICalUid.Contains(recurringId)).FirstOrDefaultAsync();
            }
        }


        public async Task<bool> DeleteEventAttendees(List<EventAttendee> eventAttendees)
        {
            using (var db = new UserContext())
            {
                db.EventAttendees.RemoveRange(eventAttendees);
                await db.SaveChangesAsync();
                return true;
            }
        }

        public async Task<bool> DeleteRecurringEvents(List<UserRecurringEvent> userRecurringEvents)
        {
            using (var db = new UserContext())
            {
                db.UserRecurringEvents.RemoveRange(userRecurringEvents);
                await db.SaveChangesAsync();
                await Task.CompletedTask;
                return true;
            }
        }

        public async Task<List<UserEvent>> GetUserEventExceptsCalId(List<string> calIds)
        {
            using (var db = new UserContext())
            {
                return await db.UserEvents.Where(a => !calIds.Contains(a.ICalUid)).ToListAsync();
            }
        }


        public async Task<bool> DeleteUserEvents(List<int> id)
        {
            using (var db = new UserContext())
            {

                var userEvents = await db.UserEvents.Where(a => id.Contains(a.Id)).ToListAsync();
                foreach (var ue in userEvents)
                {
                    ue.IsDelete = true;
                    db.Entry(ue).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                }

                return true;
            }
        }

        public async Task<bool> AddUser(Users user)
        {
            using (var db = new UserContext())
            {
                db.Users.Add(user);
                await db.SaveChangesAsync();
                return true;
            }
        }

        public async Task<bool> IsUserExist(string email)
        {
            using (var db = new UserContext())
            {
                return await db.Users.Where(a => a.Email == email).AnyAsync();
            }
        }

        #endregion
    }
}
