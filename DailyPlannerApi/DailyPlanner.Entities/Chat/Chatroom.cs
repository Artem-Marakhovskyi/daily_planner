using System.Collections.Generic;

namespace DailyPlanner.Entities.Chat
{
    public class Chatroom : Entity
    {
        public string Title { get; set; }

        public virtual ICollection<ChatroomParticipation>  ChatroomParticipants { get; set; }

        public virtual ICollection<Message> Messages { get; set; }
    }
}
