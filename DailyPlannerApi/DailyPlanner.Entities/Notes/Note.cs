using System;
namespace DailyPlanner.Entities.Notes
{
    public class Note : Entity
    {
        public string Title { get; set; }

        public string Description { get; set; }

        /// <summary>
        /// Reference to <see cref="Person"></see>
        /// </summary>
        public Guid CreatorId { get; set; }

        /// <summary>
        /// Reference to <see cref="Tag"/>
        /// </summary>
        public Guid TagId { get; set; }
    }
}
