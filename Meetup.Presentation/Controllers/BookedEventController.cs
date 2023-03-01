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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bookedEventService"></param>
        /// <param name="mapper"></param>
        public BookedEventController(IBookedEventService  bookedEventService, IMapper mapper)
        {
            _bookedEventService = bookedEventService;
            _mapper = mapper;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("{Id}")]
        public ActionResult<BookedEventReadDto> Get(int Id)
        {
            var bookedEvent = _bookedEventService.GetByIdAsync(Id);
            // Destination --> Source
            var bookedEventReadDto = _mapper.Map<BookedEventReadDto>(bookedEvent);
            return Ok(bookedEventReadDto);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bookedEvents"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<BookedEventReadDto> Post(BookedEventReadDto bookedEvents)
        {
            var CreatedBookedEvent = _mapper.Map<BookedEvent>(bookedEvents);
            var createdBookedEventService = _bookedEventService.AddAsync(CreatedBookedEvent);

            var bookedEventReadDto = _mapper.Map<BookedEventReadDto>(createdBookedEventService);

            return Ok(bookedEventReadDto);
        }

        
        [HttpGet()]
        public ActionResult<BookedEventReadDto> GetAll()
        {
            var bookedEvents = _bookedEventService.GetAllAsync();
            return Ok(bookedEvents);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bookedEvent"></param>
        /// <returns></returns>
        [HttpPut]
        public ActionResult<BookedEventCreateDto> Update(BookedEventCreateDto bookedEvent)
        {
            var bookedEventtoUpdateDto = _mapper.Map<BookedEvent>(bookedEvent);

            var _bookedEvent = _bookedEventService.UpdateAsync(bookedEventtoUpdateDto);

            var bookedEventCreateDto = _mapper.Map<BookedEventCreateDto>(_bookedEvent);

            return Ok(bookedEventCreateDto);


        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bookedEvent"></param>
        [HttpDelete]
        public void Delete(BookedEvent bookedEvent)
        {
            _bookedEventService.DeleteAsync(bookedEvent);
        }

    }
}
