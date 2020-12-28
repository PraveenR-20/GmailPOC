using System;
using System.Net.Mail;
using Gmail_POC.Data.Models;
using Google.Apis.Calendar.v3.Data;

namespace Gmail_POC.Data.Common
{
    public static partial class Common
    {

        /// <summary>
        /// This method will used for prepare event data model object
        /// </summary>
        /// <param name="eventAttendee"></param>
        /// <param name="a"></param>
        /// <param name="userEventId"></param>
        /// <returns></returns>
        public static Gmail_POC.Data.Models.EventAttendee PrepareEventAttendeeDataModel(Gmail_POC.Data.Models.EventAttendee eventAttendee, Google.Apis.Calendar.v3.Data.EventAttendee a, int userEventId)
        {
            eventAttendee.UserEventId = userEventId;
            eventAttendee.Comment = a.Comment;
            eventAttendee.DisplayName = a.DisplayName;
            eventAttendee.Email = a.Email;
            eventAttendee.Optional = Convert.ToString(a.Optional);
            eventAttendee.Organizer = Convert.ToString(a.Organizer);
            eventAttendee.Resource = Convert.ToString(a.Resource);
            eventAttendee.ResponseStatus = a.ResponseStatus;
            return eventAttendee;
        }

        /// <summary>
        /// This method will used for prepare reccuring event data model
        /// </summary>
        /// <param name="userRecurringEvent"></param>
        /// <param name="_u"></param>
        /// <param name="userEventId"></param>
        /// <returns></returns>
        public static UserRecurringEvent PrepareUserRecurringEventDataModel(UserRecurringEvent userRecurringEvent, Event _u, int userEventId)
        {
            userRecurringEvent.RecurringEventId = _u.RecurringEventId;
            userRecurringEvent.EndDatatime = _u.End.DateTime;
            userRecurringEvent.EndDate = _u.End.Date;
            userRecurringEvent.EndDatetimeraw = _u.End.DateTimeRaw;
            userRecurringEvent.EndTimezone = _u.End.TimeZone;
            userRecurringEvent.RemindersOverrides = Convert.ToString(_u.Reminders.Overrides);
            userRecurringEvent.RemindersUseDefault = Convert.ToBoolean(_u.Reminders.UseDefault);
            userRecurringEvent.UserEventId = userEventId;
            userRecurringEvent.StartDate = _u.Start.Date;
            userRecurringEvent.StartDatatime = _u.Start.DateTime;
            userRecurringEvent.StartDatetimeraw = _u.Start.DateTimeRaw;
            userRecurringEvent.StartTimezone = _u.Start.TimeZone;
            return userRecurringEvent;
        }

        /// <summary>
        /// this method will prepare event list data model
        /// </summary>
        /// <param name="evts"></param>
        /// <param name="firstEvent"></param>
        /// <returns></returns>
        public static UserEvent PrepareUserEventDataModel(UserEvent evts, Event firstEvent)
        {
            evts.AnyoneCanAddSelf = Convert.ToBoolean(firstEvent.AnyoneCanAddSelf);
            evts.AttendeesOmitted = Convert.ToBoolean(firstEvent.AttendeesOmitted);
            evts.Created = Convert.ToDateTime(firstEvent.Created);
            evts.CreatedRaw = Convert.ToString(firstEvent.CreatedRaw);
            evts.CreatorDisplayName = Convert.ToString(firstEvent.Creator.DisplayName);
            evts.CreatorEmail = Convert.ToString(firstEvent.Creator.Email);
            evts.CreatorId = Convert.ToString(firstEvent.Creator.Id);
            evts.CreatorSelf = Convert.ToBoolean(firstEvent.Creator.Self);
            evts.Description = Convert.ToString(firstEvent.Description);
            evts.EndTimeUnspecified = Convert.ToBoolean(firstEvent.EndTimeUnspecified);
            evts.GuestsCanInviteOthers = Convert.ToBoolean(firstEvent.GuestsCanInviteOthers);
            evts.GuestsCanModify = Convert.ToBoolean(firstEvent.GuestsCanModify);
            evts.GuestsCanSeeOtherGuests = Convert.ToBoolean(firstEvent.GuestsCanSeeOtherGuests);
            evts.HangoutLink = Convert.ToString(firstEvent.HangoutLink);
            evts.HtmlLink = Convert.ToString(firstEvent.HtmlLink);
            evts.ICalUid = Convert.ToString(firstEvent.ICalUID);
            evts.Kind = Convert.ToString(firstEvent.Kind);
            evts.Location = Convert.ToString(firstEvent.Location);
            evts.Locked = Convert.ToBoolean(firstEvent.Locked);
            evts.OrgDisplayName = Convert.ToString(firstEvent.Organizer.DisplayName);
            evts.OrgEmail = Convert.ToString(firstEvent.Organizer.Email);
            evts.OrgId = Convert.ToString(firstEvent.Organizer.Id);
            evts.OrgSelf = Convert.ToBoolean(firstEvent.Organizer.Self);
            
            evts.PrivateCopy = Convert.ToBoolean(firstEvent.PrivateCopy);
            evts.Recurrence = firstEvent.Recurrence != null ? string.Join(';', firstEvent.Recurrence) : null;
            evts.Sequence = Convert.ToInt32(firstEvent.Sequence);
            evts.Status = Convert.ToString(firstEvent.Status);
            evts.Summary = Convert.ToString(firstEvent.Summary);
            evts.Updated = Convert.ToDateTime(firstEvent.Updated);
            evts.UpdatedRaw = Convert.ToString(firstEvent.UpdatedRaw);
            evts.Visibility = Convert.ToString(firstEvent.Visibility);
            evts.RecurringEventId = firstEvent.RecurringEventId;
            return evts;
        }

        /// <summary>
        /// This method is used for check valid email Id
        /// </summary>
        /// <param name="emailAddress"></param>
        /// <returns></returns>
        public static bool IsValidEmail(string emailAddress)
        {            
            try
            {
                MailAddress result = new MailAddress(emailAddress);               
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

    }
}
