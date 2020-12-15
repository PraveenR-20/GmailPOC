using AutoMapper;
using Gmail_POC.Application.Interfaces;
using Gmail_POC.Data.Interfaces;
using Gmail_POC.Data.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gmail_POC.Application.Services
{
    public class UserService : IUserService
    {
        #region Private Fields

        private readonly ILogger<UserService> _logger;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        #endregion

        #region Constrouctor

        public UserService(ILogger<UserService> logger, IUserRepository userRepository, IMapper mapper)
        {
            _logger = logger;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        #endregion

        #region public methods             
        public async Task<bool> AddEvents(UserEvent events)
        {
            try
            {
                await _userRepository.AddUserEvents(events);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<UserEvent> GetUserEventByCalId(string calId, DateTime startDateTime, DateTime endDateTime)
        {
            try
            {
                return await _userRepository.GetUserEventByCalId(calId, startDateTime, endDateTime);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
        public async Task<bool> UpdateUserEvent(UserEvent userEvent)
        {
            try
            {
                await _userRepository.UpdateUserEvent(userEvent);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<bool> AddEventAttendees(List<EventAttendee> eventAttendee)
        {
            return await _userRepository.AddEventAttendees(eventAttendee);
        }

        public async Task<bool> AddRecurringEvents(List<UserRecurringEvent> userRecurringEvents)
        {
            return await _userRepository.AddRecurringEvents(userRecurringEvents);
        }

        public async Task<List<EventAttendee>> GetEventAttendeesByUserEventId(int id)
        {
            return await _userRepository.GetEventAttendeesByUserEventId(id);
        }

        public async Task<List<UserRecurringEvent>> GetRecurringEventByUserEventId(int id)
        {
            return await _userRepository.GetRecurringEventByUserEventId(id);
        }

        public async Task<UserEvent> GetUserEventByRecurringId(string recurringId)
        {
            return await _userRepository.GetUserEventByRecurringId(recurringId);
        }


        public async Task<bool> DeleteEventAttendees(List<EventAttendee> eventAttendees)
        {
            return await _userRepository.DeleteEventAttendees(eventAttendees);
        }

        public async Task<bool> DeleteRecurringEvents(List<UserRecurringEvent> userRecurringEvents)
        {
            return await _userRepository.DeleteRecurringEvents(userRecurringEvents);
        }

        public async Task<List<UserEvent>> GetUserEventExceptsCalId(List<string> calIds)
        {
            return await _userRepository.GetUserEventExceptsCalId(calIds);
        }

        public async Task<bool> DeleteUserEvents(List<int> id)
        {
            return await _userRepository.DeleteUserEvents(id);
        }

        public async Task<bool> AddUser(Users user)
        {
            return await _userRepository.AddUser(user);
        }

        public async Task<bool> IsUserExist(string email)
        {
            return await _userRepository.IsUserExist(email);
        }

        public async Task<IEnumerable<Users>> GetUsers()
        {
            return await _userRepository.GetUsers();
        }

        #endregion
    }
}
