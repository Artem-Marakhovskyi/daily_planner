using System;
namespace DailyPlanner.Entities.Chat
{
    public class ChatroomParticipation : Entity
    {
        /// <summary>
        /// Reference to <see cref="Person"/>.
        /// </summary>
        public Guid ParticipantId { get; set; }

        public Person Participant { get; set; }

        /// <summary>
        /// Reference to <see cref="Chatroom"/>.
        /// <see cref="ParticipantId"/> is participant in <see cref="ChatroomId"/>.
        /// </summary>
        public Guid ChatroomId { get; set; }

        public Chatroom Chatroom { get; set; }
    }
}
