using System;
using System.Collections.Generic;

namespace DailyPlanner.Dto.Chatroom
{
    public class ChatroomDto : EntityDto
    {
        public string Title { get; set; }

        public IEnumerable<Guid> ParticipantIds { get; set; }
    }
}
