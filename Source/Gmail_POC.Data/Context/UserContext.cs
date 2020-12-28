using Gmail_POC.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Gmail_POC.Data.Context
{
    public partial class UserContext : DbContext
    {       

        public UserContext(DbContextOptions<UserContext> options)
            : base(options)
        {
        }
        public virtual DbSet<UserModel> User { get; set; }
        public virtual DbSet<EventAttendee> EventAttendees { get; set; }
        public virtual DbSet<UserEvent> UserEvents { get; set; }
        public virtual DbSet<UserRecurringEvent> UserRecurringEvents { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EventAttendee>(entity =>
            {
                entity.Property(e => e.Comment)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("comment");

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("displayName");

                entity.Property(e => e.Email)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.Optional)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("optional");

                entity.Property(e => e.Organizer)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("organizer");

                entity.Property(e => e.Resource)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("resource");

                entity.Property(e => e.ResponseStatus)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("responseStatus");

                entity.Property(e => e.UserId)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("user_id");

                entity.HasOne(d => d.UserEvent)
                    .WithMany(p => p.EventAttendees)
                    .HasForeignKey(d => d.UserEventId)
                    .HasConstraintName("FK__EventAtte__UserE__5DCAEF64");
            });

            modelBuilder.Entity<UserEvent>(entity =>
            {
                entity.Property(e => e.AnyoneCanAddSelf).HasColumnName("anyoneCanAddSelf");

                entity.Property(e => e.AttendeesOmitted).HasColumnName("attendeesOmitted");

                entity.Property(e => e.ConferenceData)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("conferenceData");

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasColumnName("created");

                entity.Property(e => e.CreatedRaw)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("createdRaw");

                entity.Property(e => e.CreatorDisplayName)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("creator_displayName");

                entity.Property(e => e.CreatorEmail)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("creator_email");

                entity.Property(e => e.RecurringEventId)
                  .HasMaxLength(500)
                  .IsUnicode(false)
                  .HasColumnName("recurringEventId");

                entity.Property(e => e.CreatorId)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("creator_id");

                entity.Property(e => e.CreatorSelf).HasColumnName("creator_self");

                entity.Property(e => e.IsDelete).HasDefaultValueSql("((0))");

                entity.Property(e => e.Description)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.EndTimeUnspecified).HasColumnName("endTimeUnspecified");

                entity.Property(e => e.GuestsCanInviteOthers).HasColumnName("guestsCanInviteOthers");

                entity.Property(e => e.GuestsCanModify).HasColumnName("guestsCanModify");

                entity.Property(e => e.GuestsCanSeeOtherGuests).HasColumnName("guestsCanSeeOtherGuests");

                entity.Property(e => e.HangoutLink)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("hangoutLink");

                entity.Property(e => e.HtmlLink)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("htmlLink");

                entity.Property(e => e.ICalUid)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("iCalUID");

                entity.Property(e => e.Kind)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("kind");

                entity.Property(e => e.Location)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("location");

                entity.Property(e => e.Locked).HasColumnName("locked");

                entity.Property(e => e.OrgDisplayName)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("org_displayName");

                entity.Property(e => e.OrgEmail)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("org_email");

                entity.Property(e => e.OrgId)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("org_id");

                entity.Property(e => e.OrgSelf).HasColumnName("org_self");

                entity.Property(e => e.OriginalStartTime)
                    .HasColumnType("datetime")
                    .HasColumnName("originalStartTime");

                entity.Property(e => e.PrivateCopy).HasColumnName("privateCopy");

                entity.Property(e => e.Recurrence)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("recurrence");

                entity.Property(e => e.Sequence).HasColumnName("sequence");

                entity.Property(e => e.Source)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("source");

                entity.Property(e => e.Status)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("status");

                entity.Property(e => e.Summary)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("summary");

                entity.Property(e => e.Updated)
                    .HasColumnType("datetime")
                    .HasColumnName("updated");

                entity.Property(e => e.UpdatedRaw)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("updatedRaw");

                entity.Property(e => e.Visibility)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("visibility");
            });

            modelBuilder.Entity<UserRecurringEvent>(entity =>
            {
                entity.Property(e => e.EndDatatime)
                    .HasColumnType("datetime")
                    .HasColumnName("end_datatime");

                entity.Property(e => e.EndDate)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("end_date");

                entity.Property(e => e.EndDatetimeraw)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("end_datetimeraw");

                entity.Property(e => e.EndTimezone)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("end_timezone");

                entity.Property(e => e.RecurringEventId)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("recurringEventId");

                entity.Property(e => e.RemindersOverrides)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("reminders_overrides");

                entity.Property(e => e.RemindersUseDefault).HasColumnName("reminders_useDefault");

                entity.Property(e => e.StartDatatime)
                    .HasColumnType("datetime")
                    .HasColumnName("start_datatime");

                entity.Property(e => e.StartDate)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("start_date");

                entity.Property(e => e.StartDatetimeraw)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("start_datetimeraw");

                entity.Property(e => e.StartTimezone)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("start_timezone");

                entity.HasOne(d => d.UserEvent)
                    .WithMany(p => p.UserRecurringEvents)
                    .HasForeignKey(d => d.UserEventId)
                    .HasConstraintName("FK__UserRecur__UserE__60A75C0F");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
   
}
