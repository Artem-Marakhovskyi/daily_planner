using System;
using System.Collections.Generic;
using System.Text;

namespace DailyPlanner.Dto.Reminders
{
    public class ReminderSharingDto : EntityDto
    {
        public Guid SenderId { get; set; }

        public Guid ReceiverId { get; set; }

        public Guid ReminderId { get; set; }
    }
}
