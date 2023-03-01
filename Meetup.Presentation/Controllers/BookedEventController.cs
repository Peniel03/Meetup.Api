using AutoMapper;
using Meetup.BusinessLogic.Dto;
using Meetup.BusinessLogic.Interfaces;
using Meetup.DataAccess.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Meetup.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookedEventController : Controller
    {

        private readonly IBookedEventService _bookedEventService;
        private readonly IMapper _mapper;
        public BookedEventController(IBookedEventService  bookedEventService, IMapper mapper)
        {
            _bookedEventService = bookedEventService;
            _mapper = mapper;
        }

        [HttpGet("{Id}")]
        public ActionResult<BookedEventReadDto> Get(int Id)
        {
            var bookedEvent = _bookedEventService.GetByIdAsync(Id);
            // Destination --> Source
            var bookedEventReadDto = _mapper.Map<BookedEventReadDto>(bookedEvent);
            return Ok(bookedEventReadDto);

        }


        [HttpPost]
        public ActionResult<BookedEventReadDto> Post(BookedEventReadDto bookedEvents)
        {
            //Map CreatedDto to User
            var CreatedBookedEvent = _mapper.Map<BookedEvent>(bookedEvents);
            //Create User
            var createdBookedEventService = _bookedEventService.AddAsync(CreatedBookedEvent);

            //Map user to Read Dto
            var bookedEventReadDto = _mapper.Map<BookedEventReadDto>(createdBookedEventService);

            return Ok(bookedEventReadDto);
        }


        [HttpGet()]
        public ActionResult<BookedEventReadDto> GetAll()
        {
            var bookedEvents = _bookedEventService.GetAllAsync();
            return Ok(bookedEvents);
        }


        [HttpPut]
        public ActionResult<BookedEventCreateDto> Update(BookedEventCreateDto bookedEvent)
        {
            var bookedEventtoUpdateDto = _mapper.Map<BookedEvent>(bookedEvent);

            var _bookedEvent = _bookedEventService.UpdateAsync(bookedEventtoUpdateDto);

            var bookedEventCreateDto = _mapper.Map<BookedEventCreateDto>(_bookedEvent);

            return Ok(bookedEventCreateDto);


        }


        [HttpDelete]
        public void Delete(BookedEvent bookedEvent)
        {
            _bookedEventService.DeleteAsync(bookedEvent);
        }

    }
}
