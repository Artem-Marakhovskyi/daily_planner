using System;
using System.Collections.Generic;

namespace DailyPlanner.Entities.Reminders
{
    public class Reminder : Entity
    {
        public string Title { get; set; }

        public string Description { get; set; }

        /// <summary>
        /// Reference to <see cref="Person"/>
        /// </summary>
        public Guid CreatorId { get; set; }

        /// <summary>
        /// Reference to <see cref="Tag"/>
        /// </summary>
        public Guid TagId { get; set; }

        public Tag Tag { get; set; }

        public virtual ICollection<ReminderSharing> ReminderSharings { get; set; }

        public Person Creator { get; set; }
    }
}
