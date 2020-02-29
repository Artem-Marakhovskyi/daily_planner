using System;

namespace DailyPlanner.Entities.Reminders
{
    public class ReminderSharing : Entity
    {
        /// <summary>
        /// Reference to <see cref="Person"/>. They send reminder.
        /// </summary>
        public Guid SenderId { get; set; }

        /// <summary>
        /// Reference to <see cref="Person"/>. They receive reminder.
        /// </summary>
        public Guid ReceiverId { get; set; }

        /// <summary>
        /// Reference to <see cref="Reminder"/>. What is actually shared.
        /// </summary>
        public Guid ReminderId { get; set; }

        public Reminder Reminder { get; set; }

        public Person Sender { get; set; }

        public Person Receiver { get; set; }
    }
}
