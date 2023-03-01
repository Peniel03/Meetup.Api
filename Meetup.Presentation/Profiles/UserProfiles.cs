using AutoMapper;
using Meetup.BusinessLogic.Dto;
using Meetup.DataAccess.Models;

namespace Meetup.Presentation.Profiles
{
    public class UserProfiles:Profile
    {
        public UserProfiles()
        {
            // Source --> Destination

            CreateMap<User, UserReadDto>()
             .ReverseMap();
            CreateMap<UserCreateDto, User>();

        }
    }
}
