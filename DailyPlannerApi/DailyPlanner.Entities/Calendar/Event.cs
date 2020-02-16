using System;

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

        /// <summary>
        /// Reference to <see cref="Tag"/>.
        /// </summary>
        public Guid TagId { get; set; }
    }
}
