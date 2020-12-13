using System;
using System.Collections.Generic;
using System.Text;

namespace Gmail_POC.Data.Models
{
    public partial class UserRecurringEvent
    {
        public int Id { get; set; }
        public int? UserEventId { get; set; }
        public string StartDate { get; set; }
        public string StartDatetimeraw { get; set; }
        public DateTime? StartDatatime { get; set; }
        public string StartTimezone { get; set; }
        public string EndDate { get; set; }
        public string EndDatetimeraw { get; set; }
        public DateTime? EndDatatime { get; set; }
        public string EndTimezone { get; set; }
        public string RecurringEventId { get; set; }
        public string RemindersOverrides { get; set; }
        public bool? RemindersUseDefault { get; set; }

        public virtual UserEvent UserEvent { get; set; }
    }
}
