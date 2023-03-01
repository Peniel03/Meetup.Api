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
    public class EventRepository : IEventRepository
    {

        private readonly MeetupContext _meetupContext;
        public EventRepository(MeetupContext meetupContext)
        {
            _meetupContext = meetupContext;

        }
        public void AddAsync(Event Event)
        {
            _meetupContext.Add(Event);
        }

        public void DeleteAsync(Event Event)
        {
            _meetupContext.Remove(Event);
        }

        public Task<List<Event>> GetAllAsync()
        {
            return _meetupContext.Events
                              .AsNoTracking()
                              .ToListAsync();
        }

        public Task<Event?> GetByIdAsync(int Id)
        {
            return _meetupContext.Events
                           .Where(x => x.Id == Id)
                           .AsNoTracking()
                           .FirstOrDefaultAsync();
        }
        public Task SavechangesAsync()
        {
            return _meetupContext.SaveChangesAsync();
        }
        public void UpdateAsync(Event Event)
        {
            _meetupContext.Update(Event);
        }
    }
}
