using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meetup.BusinessLogic.Exceptions
{
    public class NotFoundException:Exception
    {
        public NotFoundException()
        {

        }
        public NotFoundException(string message) : base(message)
        {

        }
        public NotFoundException(string message, Exception ex) : base(message, ex)
        {

        }
    }
}
