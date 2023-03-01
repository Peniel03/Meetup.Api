using Meetup.BusinessLogic.Accounts.Token;
using Meetup.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meetup.BusinessLogic.Interfaces
{
    public interface IAuthenticateService
    {
        Task<Tokens> Authenticate(string email, string password);
        Task<User> ValidateUserAsync(string email, string password);
    }
}
