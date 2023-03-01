using Meetup.BusinessLogic.Accounts.Token;
using Meetup.BusinessLogic.Exceptions;
using Meetup.BusinessLogic.Interfaces;
using Meetup.DataAccess.Interfaces;
using Meetup.DataAccess.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Meetup.BusinessLogic.Accounts.Authentication
{
    public class AuthenticateService : IAuthenticateService
    {

        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;
        public AuthenticateService(IConfiguration configuration, IUserRepository userRepository)
        {
            _configuration = configuration;
            _userRepository = userRepository;
        }
        public async Task<Tokens> Authenticate(string email, string password)
        {
            var _user = await ValidateUserAsync(email, password);

            //we have Authenticated
            //Generate Json Web Token 
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.UTF8.GetBytes(_configuration["JWT:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Email ,email)
                }),
                Expires = DateTime.UtcNow.AddMinutes(10),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey),
                SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return new Tokens { Token = tokenHandler.WriteToken(token) };
        }

        public async Task<User> ValidateUserAsync(string email, string password)
        {
            var userChecked = await _userRepository.GetUserByEmailAsync(email);

            if (userChecked is null || userChecked.Password == password)
            {
                throw new NotFoundException("This user does exist");
            }

            return userChecked;
        }
    }
}
