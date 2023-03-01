using Meetup.BusinessLogic.Exceptions;
using Meetup.BusinessLogic.Interfaces;
using Meetup.DataAccess.Interfaces;
using Meetup.DataAccess.Models;
using Meetup.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meetup.BusinessLogic.RepositoriesServices
{
    public class BookedEventService : IBookedEventService
    {
        private readonly IBookedEventRepository _bookedEventRepository;
        public BookedEventService(IBookedEventRepository bookedEventRepository)
        {
            _bookedEventRepository = bookedEventRepository;
        }

        public async Task<BookedEvent> AddAsync(BookedEvent bookedEvent)
        {
           var bookedEventChecked = await _bookedEventRepository.GetByIdAsync(bookedEvent.Id);
            if (bookedEventChecked is not null)
              {
                   throw new AlreadyExistException("This BookedEvent already exist");
              }

            _bookedEventRepository.AddAsync(bookedEvent);
            await _bookedEventRepository.SavechangesAsync();

            return bookedEvent;
            
        }

        public async Task<BookedEvent> DeleteAsync(BookedEvent bookedEvent)
        {
            var bookedEventChecked = await _bookedEventRepository.GetByIdAsync(bookedEvent.Id);
            if (bookedEventChecked is null)
            {
                throw new NotFoundException("This BookedEvent does not exist");
            }
            _bookedEventRepository.DeleteAsync(bookedEventChecked);
            await _bookedEventRepository.SavechangesAsync();

            return bookedEvent;
        }

        public async Task<List<BookedEvent>> GetAllAsync()
        {
            var bookedEvents = await _bookedEventRepository.GetAllAsync();
            if (bookedEvents is null)
            {
                throw new NotFoundException("There are no registered BookedEvents yet");
            }
            return bookedEvents;
        }

        public async Task<BookedEvent?> GetByIdAsync(int Id)
        {
            var BookedEvent = await _bookedEventRepository.GetByIdAsync(Id);
            if (BookedEvent is null)
            {
                throw new NotFoundException("This Event does not exist");
            }
            return BookedEvent;
        }

        public Task SavechangesAsync()
        {
            return _bookedEventRepository.SavechangesAsync();
        }

        public async Task<BookedEvent> UpdateAsync(BookedEvent bookedEvent)
        {
            var bookedEventChecked = await _bookedEventRepository.GetByIdAsync(bookedEvent.Id);
            if (bookedEventChecked is null)
            {
                throw new NotFoundException("This Event does not exist");

            }

            bookedEventChecked.ParticipantName = bookedEvent.ParticipantName;
            bookedEventChecked.ParticipantEmail = bookedEvent.ParticipantEmail;
            bookedEventChecked.ParticipantPhoneNumber = bookedEvent.ParticipantPhoneNumber;
            bookedEventChecked.EventName = bookedEvent.EventName;
            bookedEventChecked.Description = bookedEvent.Description;
            bookedEventChecked.Confirmation = bookedEvent.Confirmation;
            bookedEventChecked.Organiser = bookedEvent.Organiser;
            bookedEventChecked.Time = bookedEvent.Time;
            bookedEventChecked.Location = bookedEvent.Location;

            _bookedEventRepository.UpdateAsync(bookedEventChecked);
            await _bookedEventRepository.SavechangesAsync();

            return bookedEvent;
        }
    }
}
