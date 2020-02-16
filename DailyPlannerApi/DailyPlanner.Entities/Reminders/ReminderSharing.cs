using System;

namespace DailyPlanner.Entities.Reminders
{
    public class ReminderSharing : Entity
    {
        /// <summary>
        /// Reference to <see cref="Person"/>. They send reminder.
        /// </summary>
        public Guid CreatorId { get; set; }

        /// <summary>
        /// Reference to <see cref="Person"/>. They receive reminder.
        /// </summary>
        public Guid ReceiverId { get; set; }

        /// <summary>
        /// Reference to <see cref="Reminder"/>. What is actually shared.
        /// </summary>
        public Guid ReminderId { get; set; }
    }
}
