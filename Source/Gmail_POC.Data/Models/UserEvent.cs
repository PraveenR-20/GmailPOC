using System;
using System.Collections.Generic;

namespace Gmail_POC.Data.Models
{
    public partial class UserEvent
    {
        public UserEvent()
        {
            EventAttendees = new HashSet<EventAttendee>();
            UserRecurringEvents = new HashSet<UserRecurringEvent>();
        }

        public int Id { get; set; }
        public bool? AnyoneCanAddSelf { get; set; }
        public bool? AttendeesOmitted { get; set; }
        public string ConferenceData { get; set; }

        public bool? IsDelete { get; set; }
        public string CreatedRaw { get; set; }
        public DateTime? Created { get; set; }
        public string Description { get; set; }
        public bool? EndTimeUnspecified { get; set; }
        public bool? GuestsCanInviteOthers { get; set; }
        public bool? GuestsCanModify { get; set; }
        public bool? GuestsCanSeeOtherGuests { get; set; }
        public string HangoutLink { get; set; }
        public string HtmlLink { get; set; }
        public string ICalUid { get; set; }
        public string Kind { get; set; }
        public string Location { get; set; }
        public bool? Locked { get; set; }
        public DateTime? OriginalStartTime { get; set; }
        public bool? PrivateCopy { get; set; }
        public string Recurrence { get; set; }
        public int? Sequence { get; set; }
        public string Source { get; set; }
        public string Status { get; set; }
        public string Summary { get; set; }
        public string Visibility { get; set; }
        public string UpdatedRaw { get; set; }
        public DateTime? Updated { get; set; }
        public string CreatorDisplayName { get; set; }
        public string CreatorEmail { get; set; }
        public string CreatorId { get; set; }
        public bool? CreatorSelf { get; set; }
        public string OrgDisplayName { get; set; }
        public string OrgEmail { get; set; }
        public string OrgId { get; set; }
        public bool? OrgSelf { get; set; }
        public string RecurringEventId { get; set; }
        public virtual ICollection<EventAttendee> EventAttendees { get; set; }
        public virtual ICollection<UserRecurringEvent> UserRecurringEvents { get; set; }
    }
}
