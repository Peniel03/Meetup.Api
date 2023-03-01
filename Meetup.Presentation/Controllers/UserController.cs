using AutoMapper;
using Meetup.BusinessLogic.Dto;
using Meetup.BusinessLogic.Interfaces;
using Meetup.DataAccess.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Meetup.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {

        private readonly IAuthenticateService _authenticateService;
        private readonly IUserService _userService;
         private readonly IMapper _mapper;
        public UserController(IAuthenticateService authenticateService, IUserService userService, IMapper mapper)
        {
            _authenticateService = authenticateService;
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet("{Id}")]
        public ActionResult<UserReadDto> Get(int Id)
        {
            var user = _userService.GetByIdAsync(Id);
            // Destination --> Source
            var userReadDto = _mapper.Map<UserReadDto>(user);
            return Ok(userReadDto);

        }



        [HttpPost]
        public ActionResult<UserReadDto> Post(UserReadDto users)
        {
            //Map CreatedDto to User
            var CreatedUser = _mapper.Map<User>(users);

            //Create User
            var createdUserService = _userService.AddAsync(CreatedUser);

            //Map user to Read Dto
            var userReadDto = _mapper.Map<UserReadDto>(createdUserService);

            return Ok(userReadDto);
        }


        [HttpGet()]
        public ActionResult<UserReadDto> GetAll()
        {
            var Users = _userService.GetAllAsync();
            return Ok(Users);
        }


        [AllowAnonymous]
        [HttpPost("authenticate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        // need to replace usersdata by authenticate 
        public IActionResult Aunthenticate(string email, string password)
        {
            var token = _authenticateService.Authenticate(email, password);
            if (token == null)
            {
                return Unauthorized();
            }

            return Ok(token);
        }


        [HttpPut]
        public ActionResult<UserCreateDto> Update(UserCreateDto user)
        {
            var usertoUpdateDto = _mapper.Map<User>(user);

            var _user = _userService.UpdateAsync(usertoUpdateDto);

            var userCreateDto = _mapper.Map<UserCreateDto>(_user);

            return Ok(_user);
        }

        [HttpDelete]
        public void Delete(User user)
        {
            _userService.DeleteAsync(user);
        }
 
        
    }
}
