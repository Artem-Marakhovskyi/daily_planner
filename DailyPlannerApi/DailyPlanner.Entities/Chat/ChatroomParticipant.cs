using System;
namespace DailyPlanner.Entities.Chat
{
    public class ChatroomParticipant : Entity
    {
        /// <summary>
        /// Reference to <see cref="Person"/>.
        /// </summary>
        public Guid ParticipantId { get; set; }

        /// <summary>
        /// Reference to <see cref="Chatroom"/>.
        /// <see cref="ParticipantId"/> is participant in <see cref="ChatroomId"/>.
        /// </summary>
        public Guid ChatroomId { get; set; }
    }
}
