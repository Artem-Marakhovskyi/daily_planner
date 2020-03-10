using System;

namespace DailyPlanner.Dto.Reminders
{
    public class ReminderDto : EntityDto
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public Guid CreatorId { get; set; }

        public string Tag { get; set; }
    }
}
