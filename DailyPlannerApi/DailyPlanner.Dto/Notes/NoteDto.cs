using System;

namespace DailyPlanner.Dto.Notes
{
    public class NoteDto : EntityDto
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public Guid CreatorId { get; set; }

        public string Tag { get; set; }
    }
}

