using System;
using System.Collections.Generic;

namespace DailyPlanner.Entities.Calendar
{
    public class Event : Entity
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public DateTimeOffset StartTime { get; set; }

        public DateTimeOffset EndTime { get; set; }

        /// <summary>
        /// Reference to <see cref="Person"/>. They create event.
        /// </summary>
        public Guid CreatorId { get; set; }

        public Person Creator { get; set; }

        /// <summary>
        /// Reference to <see cref="Tag"/>.
        /// </summary>
        public Guid TagId { get; set; }

        public Tag Tag { get; set; }

        public virtual ICollection<EventSharing> EventSharings { get; set; }
    }
}
