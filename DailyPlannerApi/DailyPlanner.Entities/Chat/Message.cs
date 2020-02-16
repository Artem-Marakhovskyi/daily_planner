using System;
namespace DailyPlanner.Entities.Chat
{
    public class Message : Entity
    {
        /// <summary>
        /// Reference to <see cref="Person"/>. They send message.
        /// </summary>
        public Guid SenderId { get; set; }

        /// <summary>
        /// Reference to <see cref="Chatroom"/>. Messages are sent to this chatroom.
        /// </summary>
        public Guid ChartoomId { get; set; }

        public MessageType Type { get; set; }

        public string Text { get; set; }

        public DateTimeOffset DateSent { get; set; }

        public DateTimeOffset DateReceived { get; set; }

        public DateTimeOffset DateRead { get; set; }
    }
}
