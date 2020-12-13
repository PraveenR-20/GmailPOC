using System;
namespace Gmail_POC.Data.Models
{
    class UserEventsViewModel
    {
        public int Id { get; set; }
        public string anyoneCanAddSelf { get; set; }
        public string attachments { get; set; }
        public string attendees { get; set; }
        public string attendeesOmitted { get; set; }
        public string colorId { get; set; }
        public string conferenceData { get; set; }
        public DateTime createdRaw { get; set; }
        public DateTime created { get; set; }
        public string description { get; set; }
        public string endTimeUnspecified { get; set; }
        public string eTag { get; set; }
        public string extendedProperties { get; set; }
        public string gadget { get; set; }
        public string guestsCanInviteOthers { get; set; }
        public string guestsCanModify { get; set; }
        public string guestsCanSeeOtherGuests { get; set; }
        public string hangoutLink { get; set; }
        public string htmlLink { get; set; }
        public string iCalUID { get; set; }
        public string kind { get; set; }
        public string location { get; set; }
        public string locked { get; set; }
        public string originalStartTime { get; set; }
        public string privateCopy { get; set; }
        public string recurrence { get; set; }
        public string recurringEventId { get; set; }
        public int sequence { get; set; }
        public string source { get; set; }
        public string status { get; set; }
        public string summary { get; set; }
        public string transparency { get; set; }
        public string visibility { get; set; }
        public DateTime updatedRaw { get; set; }
        public DateTime updated { get; set; }
        public string creator_displayName { get; set; }
        public string creator_email { get; set; }
        public string creator_id { get; set; }
        public bool creator_self { get; set; }
        public string end_date { get; set; }
        public string end_dateTimeRaw { get; set; }
        public string end_dateTime { get; set; }
        public string end_timeZone { get; set; }
        public string end_eTag { get; set; }
        public string org_displayName { get; set; }
        public string org_email { get; set; }
        public string org_id { get; set; }
        public string org_self { get; set; }
        public string reminders_overrides { get; set; }
        public bool reminders_useDefault { get; set; }
        public string start_date { get; set; }
        public string start_dateTimeRaw { get; set; }
        public string start_dateTime { get; set; }
        public string start_timeZone { get; set; }
        public string start_eTag { get; set; }
    }
}
