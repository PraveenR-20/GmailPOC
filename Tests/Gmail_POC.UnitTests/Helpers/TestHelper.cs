using Gmail_POC.Data.Context;
using System;
using System.Linq;


namespace Gmail_POC.UnitTests.Helpers
{
    internal class TestHelper
    {
        internal static void AddDummyEventData(UserContext userContext)
        {
            if (userContext.UserEvents.Count() == 0)
            {
                userContext.UserEvents.AddRange(new Gmail_POC.Data.Models.UserEvent
                {
                    Id = 1,
                    AnyoneCanAddSelf = false,
                    AttendeesOmitted = false,
                    Created = DateTime.Now,
                    CreatedRaw = "",
                    CreatorSelf = false,
                    EndTimeUnspecified = false,
                    GuestsCanInviteOthers = false,
                    GuestsCanModify = false,
                    GuestsCanSeeOtherGuests = false,
                    HangoutLink = "https://meet.google.com/wtz-nbmp-bbp",
                    HtmlLink = "https://www.google.com/calendar/event?eid=NmltbmdnZ2NsYmw2bHFwOXNnZThkbjQ2ZGxfMjAyMDEyMjhUMDgzMDAwWiByYWFodWxzaHJpdmFzdGF2YTBAbQ",
                    ICalUid = "6imngggclbl6lqp9sge8dn46dl@google.com",
                    Kind = "calendar#event",
                    Location = "Mumbai, Maharashtra, India",
                    Locked = false,
                    OrgSelf = false,
                    PrivateCopy = false,
                    Recurrence = null,
                    Sequence = 1,
                    Status = "confirmed",
                    Summary = "Test Event",
                    RecurringEventId = "6imngggclbl6lqp9sge8dn46dl"
                });
                try
                {
                    userContext.SaveChanges();
                }
                catch
                {
                }
            }
        }
        internal static void AddDummyEventAttendee(UserContext userContext)
        {
            if (userContext.EventAttendees.Count() == 0)
            {
                userContext.EventAttendees.AddRange(new Gmail_POC.Data.Models.EventAttendee
                {
                    Id = 1,
                    UserEventId = 1,
                    Email = "test01@gmail.com",
                    ResponseStatus = "accepted"
                },
                new Gmail_POC.Data.Models.EventAttendee
                {
                    Id = 2,
                    UserEventId = 1,
                    Email = "test02@gmail.com",
                    ResponseStatus = "accepted"
                });
                try
                {
                    userContext.SaveChanges();
                }
                catch
                {
                }
            }
        }
        internal static void AddDummyRecurringEvent(UserContext userContext)
        {
            if (userContext.UserRecurringEvents.Count() == 0)
            {
                userContext.UserRecurringEvents.AddRange(
                    new Gmail_POC.Data.Models.UserRecurringEvent
                    {
                        Id = 1,
                        UserEventId = 1,
                        StartTimezone = "Asia/Kolkata",
                        EndTimezone = "Asia/Kolkata",
                        RecurringEventId = "6imngggclbl6lqp9sge8dn46dl",
                        RemindersUseDefault = true,
                        StartDatatime = DateTime.Now,
                        EndDatatime = DateTime.Now.AddDays(2),
                        StartDatetimeraw = "2020-12-29T14:00:00+05:30",
                        EndDatetimeraw = "2020-12-29T15:00:00+05:30"
                    },
                    new Gmail_POC.Data.Models.UserRecurringEvent
                    {
                        Id = 2,
                        UserEventId = 1,
                        StartTimezone = "Asia/Kolkata",
                        EndTimezone = "Asia/Kolkata",
                        RecurringEventId = "6imngggclbl6lqp9sge8dn46dl",
                        RemindersUseDefault = true,
                        StartDatatime = DateTime.Now,
                        EndDatatime = DateTime.Now.AddDays(2),
                        StartDatetimeraw = "2020-12-29T14:00:00+05:30",
                        EndDatetimeraw = "2020-12-29T15:00:00+05:30"
                    });
                try
                {
                    userContext.SaveChanges();
                }
                catch
                {
                }
            }
        }


    }
}