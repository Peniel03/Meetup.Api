using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meetup.BusinessLogic.Dto
{
    public class BookedEventReadDto
    {
        public string ParticipantName { get; set; } = string.Empty;
        public string ParticipantEmail { get; set; } = string.Empty;
        public string ParticipantPhoneNumber { get; set; } = string.Empty;
        public string EventName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Boolean Confirmation { get; set; }
        public string Organiser { get; set; } = string.Empty;
        public DateTime Time { get; set; }
        public string Location { get; set; } = string.Empty;
    }
}
