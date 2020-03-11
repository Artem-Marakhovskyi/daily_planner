using System;
using System.Collections.Generic;
using System.Text;

namespace DailyPlanner.Dto.Chat
{
    public class ChatroomParticipationDto : EntityDto
    {
        public Guid ParticipantId { get; set; }

        public Guid ChatroomId { get; set; }
    }
}
