using System;
namespace DailyPlanner.Dto.Calendar
{
    public class EventSharingDto : EntityDto
    {
        public Guid CreatorId { get; set; }

        public Guid ReceiverId { get; set; }

        public Guid EventId { get; set; }

        public bool IsReceiverRequired { get; set; }
    }
}
