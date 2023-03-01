using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meetup.DataAccess.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string EventName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Organiser { get; set; } = string.Empty;
        public DateTime Time { get; set; }
        public string Location { get; set; } = string.Empty;

        //Relationships
        public User User { get; set; }
        public int UserId { get; set; }
        public ICollection<BookedEvent> BookedEvents { get; set; }


        public Event()
        {

        }
    }
}
