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
    public class EventService : IEventService
    { 
        private readonly IEventRepository _eventRepository;
        public EventService(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task<Event> AddAsync(Event Event)
        {
            var eventChecked = await _eventRepository.GetByIdAsync(Event.Id);
            if(eventChecked is not null)
            {
                throw new AlreadyExistException("This Event already exist");
            }
            _eventRepository.AddAsync(Event);
            await _eventRepository.SavechangesAsync();

            return Event;
        }

        public async Task<Event> DeleteAsync(Event Event)
        {
            var eventChecked = await _eventRepository.GetByIdAsync(Event.Id);
            if(eventChecked is null)
            {
                throw new NotFoundException("This Event does not exist");
            }
            _eventRepository.DeleteAsync(eventChecked);
            await _eventRepository.SavechangesAsync();

            return Event;
        }

        public async Task<List<Event>> GetAllAsync()
        {
            var Events = await _eventRepository.GetAllAsync();
            if(Events is null)
            {
                throw new NotFoundException("There are no registered Events yet");
            }
            return Events;
        }

        public async Task<Event?> GetByIdAsync(int Id)
        {
            var Event = await _eventRepository.GetByIdAsync(Id);
            if(Event is null)
            {
                throw new NotFoundException("This Event does not exist");
            }
            return Event;
        }
        public Task SavechangesAsync()
        {
            return _eventRepository.SavechangesAsync();
        }
        public async Task<Event> UpdateAsync(Event Event)
        {
            var eventChecked = await _eventRepository.GetByIdAsync(Event.Id);
            if(eventChecked is null)
            {
                throw new NotFoundException("This Event does not exist");

            }

            eventChecked.EventName = Event.EventName;
            eventChecked.Description = Event.Description;
            eventChecked.Organiser = Event.Organiser;
            eventChecked.Time = Event.Time;
            eventChecked.Location = Event.Location;

            _eventRepository.UpdateAsync(eventChecked);
            await _eventRepository.SavechangesAsync();

            return Event;
        }

    }
}
