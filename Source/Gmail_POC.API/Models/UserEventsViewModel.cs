using System;

namespace Gmail_POC.API.Models
{
    public enum UserEventStatus
    {
        confirmed, tentative, cancelled
    }

    public class Creator
    {
        public string displayName { get; set; }
        public string email { get; set; }
        public string id { get; set; }
        public bool self { get; set; }
    }

    public class End
    {
        public string date { get; set; }
        public string dateTimeRaw { get; set; }
        public string dateTime { get; set; }
        public string timeZone { get; set; }
        public string eTag { get; set; }
    }

    public class Organizer
    {
        public string displayName { get; set; }
        public string email { get; set; }
        public string id { get; set; }
        public bool self { get; set; }
    }

    public class Reminders
    {
        public string overrides { get; set; }
        public bool useDefault { get; set; }
    }

    public class Start
    {
        public string date { get; set; }
        public string dateTimeRaw { get; set; }
        public string dateTime { get; set; }
        public string timeZone { get; set; }
        public string eTag { get; set; }
    }

    public class UserEventsViewModel
    {
        public string anyoneCanAddSelf { get; set; }
        public string attachments { get; set; }
        public string attendees { get; set; }
        public string attendeesOmitted { get; set; }
        public string colorId { get; set; }
        public string conferenceData { get; set; }
        public DateTime createdRaw { get; set; }
        public DateTime created { get; set; }
        public Creator creator { get; set; }
        public string description { get; set; }
        public End end { get; set; }
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
        public string id { get; set; }
        public string kind { get; set; }
        public string location { get; set; }
        public string locked { get; set; }
        public Organizer organizer { get; set; }
        public string originalStartTime { get; set; }
        public string privateCopy { get; set; }
        public string recurrence { get; set; }
        public string recurringEventId { get; set; }
        public Reminders reminders { get; set; }
        public int sequence { get; set; }
        public string source { get; set; }
        public Start start { get; set; }
        public string status { get; set; }
        public string summary { get; set; }
        public string transparency { get; set; }
        public DateTime updatedRaw { get; set; }
        public DateTime updated { get; set; }
        public string visibility { get; set; }
    }

}

