using AutoMapper;
using Meetup.BusinessLogic.Dto;
using Meetup.BusinessLogic.Interfaces;
using Meetup.BusinessLogic.RepositoriesServices;
using Meetup.DataAccess.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Meetup.Presentation.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : Controller
    {
        private readonly IEventService _eventService;
        private readonly IMapper _mapper;
        public EventController(IEventService eventService,IMapper mapper)
        {
            _eventService = eventService;
            _mapper = mapper;
        }

        [HttpGet("{Id}")]
        public ActionResult<EventReadDto> Get(int Id)
        {
            var Event = _eventService.GetByIdAsync(Id);
            // Destination --> Source
            var eventReadDto = _mapper.Map<EventReadDto>(Event);
            return Ok(eventReadDto);

        }


         

        [HttpPost]
        public ActionResult<EventReadDto> Post(EventReadDto Events)
        {
            //Map CreatedDto to User
            var CreatedEvent = _mapper.Map<Event>(Events);

            //Create User
            var createdEventService = _eventService.AddAsync(CreatedEvent);

            //Map user to Read Dto
            var eventReadDto = _mapper.Map<EventReadDto>(createdEventService);

            return Ok(eventReadDto);
        }


        [HttpGet()]
        public ActionResult<EventReadDto> GetAll()
        {
            var Events = _eventService.GetAllAsync();
            return Ok(Events);
        }


        [HttpPut]
        public ActionResult<EventCreateDto> Update(EventCreateDto Event)
        {
            var eventtoUpdateDto = _mapper.Map<Event>(Event);

            var _event = _eventService.UpdateAsync(eventtoUpdateDto);

            var eventCreateDto = _mapper.Map<EventCreateDto>(_event);

            return Ok(eventCreateDto);

        }


        [HttpDelete]
        public void Delete(Event Event)
        {
            _eventService.DeleteAsync(Event);
        }


    }
}
