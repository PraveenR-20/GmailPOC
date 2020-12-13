using System;
using System.Collections.Generic;
using System.Text;

namespace Gmail_POC.Data.Models
{
    public partial class EventAttendee
    {
        public int Id { get; set; }
        public int? UserEventId { get; set; }
        public string Comment { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string UserId { get; set; }
        public string Optional { get; set; }
        public string Organizer { get; set; }
        public string Resource { get; set; }
        public string ResponseStatus { get; set; }

        public virtual UserEvent UserEvent { get; set; }
    }
}
