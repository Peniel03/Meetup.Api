using Meetup.BusinessLogic.Exceptions;
using Meetup.BusinessLogic.Interfaces;
using Meetup.DataAccess.Interfaces;
using Meetup.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meetup.BusinessLogic.RepositoriesServices
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> AddAsync(User user)
        {
            var userChecked = await _userRepository.GetByIdAsync(user.Id);
            if(userChecked is not null)
            {
                throw new AlreadyExistException("This User already exist");
            }
            _userRepository.AddAsync(user);
            await _userRepository.SavechangesAsync();

            return user;
         }

        public async Task<User> DeleteAsync(User user)
        {
            var userChecked = await _userRepository.GetByIdAsync(user.Id);
            if(userChecked is null)
            {
                throw new NotFoundException("This User does not exist");
            }
            _userRepository.DeleteAsync(userChecked);
            await _userRepository.SavechangesAsync();

            return user;

        }

        public async Task<List<User>> GetAllAsync()
        {
            var users = await _userRepository.GetAllAsync();
            if(users is null)
            {
                throw new NotFoundException("There are no registered users yet");
            }
            return users;
        }

        public async Task<User?> GetByIdAsync(int Id)
        {
            var user = await _userRepository.GetByIdAsync(Id);
            if(user is null)
            {
                throw new NotFoundException("This User does not exist");
            }
            return user;
        }

        public async Task<User?> GetUserByEmailAsync(string Email)
        {
            var user = await _userRepository.GetUserByEmailAsync(Email);
            if (user is null)
            {
                throw new NotFoundException("This User does not exist");
            }
            return user;
        }

        public Task SavechangesAsync()
        {
            return _userRepository.SavechangesAsync();
        }

        public async Task<User> UpdateAsync(User user)
        {
            var userChecked = await _userRepository.GetByIdAsync(user.Id);
            if(userChecked is null)
            {
                throw new NotFoundException("This User does not exist");
            }

            userChecked.Name = user.Name;
            userChecked.Password = user.Password;
            userChecked.Email = user.Email;
            userChecked.PhoneNumber = user.PhoneNumber;

            _userRepository.UpdateAsync(userChecked);
            await _userRepository.SavechangesAsync();

            return user;
        }

    }
}
