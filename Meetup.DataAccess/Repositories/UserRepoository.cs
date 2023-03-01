using Meetup.DataAccess.DataContext;
using Meetup.DataAccess.Interfaces;
using Meetup.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meetup.DataAccess.Repositories
{
    public class UserRepoository : IUserRepository
    {
        private readonly MeetupContext _meetupContext;
        public UserRepoository(MeetupContext meetupContext)
        {
            _meetupContext = meetupContext;

        }
        public void AddAsync(User user)
        {
            _meetupContext.Add(user);
        }

        public void DeleteAsync(User user)
        {
            _meetupContext.Remove(user);
        }

        public Task<List<User>> GetAllAsync()
        {
            return _meetupContext.Users
                   .AsNoTracking()
                   .ToListAsync();
        }

        public Task<User?> GetByIdAsync(int Id)
        {
            return _meetupContext.Users
                .Where(x => x.Id == Id)
                .AsNoTracking()
                .FirstOrDefaultAsync();
         }

        public Task<User?> GetUserByEmailAsync(string Email)
        {
            return _meetupContext.Users
                            .Where(u => u.Email == Email)
                            .AsNoTracking()
                            .FirstOrDefaultAsync();
        }

        public Task SavechangesAsync()
        {
            return _meetupContext.SaveChangesAsync();
        }
        public void UpdateAsync(User user)
        {
            _meetupContext.Update(user);
        }

    }
}
