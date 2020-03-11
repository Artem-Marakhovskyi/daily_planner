using System;

namespace DailyPlanner.Dto.Chat
{
    public class MessageDto : EntityDto
    {
        public Guid SenderId { get; set; }

        public Guid ChatoomId { get; set; }

        public MessageDtoType Type { get; set; }

        public Guid? ReferenceId { get; set; }

        public string Text { get; set; }

        public DateTimeOffset DateSent { get; set; }

        public DateTimeOffset DateReceived { get; set; }

        public DateTimeOffset DateRead { get; set; }

        public string Tag { get; set; }
    }
}
