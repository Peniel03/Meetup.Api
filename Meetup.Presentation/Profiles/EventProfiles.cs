using AutoMapper;
using Meetup.BusinessLogic.Dto;
using Meetup.DataAccess.Models;

namespace Meetup.Presentation.Profiles
{
    public class EventProfiles:Profile
    {

        public EventProfiles()
        {
            // Source --> Destination

            CreateMap<Event, EventReadDto>()
                 .ReverseMap();
            CreateMap<EventCreateDto, Event>();
        }
    }
}
