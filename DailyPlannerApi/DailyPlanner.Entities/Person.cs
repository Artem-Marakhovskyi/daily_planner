using DailyPlanner.Entities.Calendar;
using DailyPlanner.Entities.Chat;
using DailyPlanner.Entities.Notes;
using DailyPlanner.Entities.Reminders;
using System.Collections.Generic;

namespace DailyPlanner.Entities
{
    public class Person : Entity
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string PasswordHash { get; set; }

        public byte[] Salt { get; set; }



        public virtual ICollection<Note> Notes { get; set; }

        public virtual ICollection<Reminder> Reminders { get; set; }

        public virtual ICollection<ReminderSharing> RemindersSent { get; set; }

        public virtual ICollection<ReminderSharing> RemindersReceived { get; set; }

        public virtual ICollection<Event> Events { get; set; }

        public virtual ICollection<EventSharing> EventsReceived { get; set; }

        public virtual ICollection<EventSharing> EventsSent { get; set; }

        public virtual ICollection<ChatroomParticipation> ChatroomParticipations { get; set; }
    }
}
