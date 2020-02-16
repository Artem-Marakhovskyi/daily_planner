using System;
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
    }
}
