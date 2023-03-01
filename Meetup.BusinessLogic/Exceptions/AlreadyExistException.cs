using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meetup.BusinessLogic.Exceptions
{
    public class AlreadyExistException:Exception
    {
        public AlreadyExistException()
        {

        }
        public AlreadyExistException(string message) : base(message)
        {

        }
        public AlreadyExistException(string message, Exception ex) : base(message, ex)
        {

        }
    }
}
