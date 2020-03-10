using System;
using System.Collections.Generic;
using System.Text;

namespace DailyPlanner.Dto.Calendars
{
    public class EventDto : EntityDto
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public DateTimeOffset StartTime { get; set; }

        public DateTimeOffset EndTime { get; set; }

        public Guid CreatorId { get; set; }

        public string Tag { get; set; }
    }
}
