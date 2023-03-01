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
    public class BookedEventRepository:IBookedEventRepository
    {
        private readonly MeetupContext _meetupContext;
        public BookedEventRepository(MeetupContext meetupContext)
        {
            _meetupContext = meetupContext;

        }

        public void AddAsync(BookedEvent bookedEvent)
        {
            _meetupContext.Add(bookedEvent);
        }

        public void DeleteAsync(BookedEvent bookedEvent)
        {
            _meetupContext.Remove(bookedEvent);
        }

        public Task<List<BookedEvent>> GetAllAsync()
        {
            return _meetupContext.BookedEvents
                   .AsNoTracking()
                   .ToListAsync();
        }

        public Task<BookedEvent?> GetByIdAsync(int Id)
        {
            return _meetupContext.BookedEvents
                   .Where(x => x.Id == Id)
                   .AsNoTracking()
                   .FirstOrDefaultAsync();
        }

        public Task SavechangesAsync()
        {
            return _meetupContext.SaveChangesAsync();
        }

        public void UpdateAsync(BookedEvent bookedEvent)
        {
            _meetupContext.Update(bookedEvent);
        }

    }
}
