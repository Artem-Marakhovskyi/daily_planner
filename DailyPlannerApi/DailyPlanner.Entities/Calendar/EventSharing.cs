using System;
namespace DailyPlanner.Entities.Calendar
{
    public class EventSharing : Entity
    {
        /// <summary>
        /// Reference to <see cref="Person"/>. They send event.
        /// </summary>
        public Guid CreatorId { get; set; }

        /// <summary>
        /// Reference to <see cref="Person"/>. They receive event.
        /// </summary>
        public Guid ReceiverId { get; set; }

        /// <summary>
        /// Reference to <see cref="Event"/>. What is actually shared.
        /// </summary>
        public Guid EventId { get; set; }

        public bool IsReceiverRequired { get; set; }
    }
}
