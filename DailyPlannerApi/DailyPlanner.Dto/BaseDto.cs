using System;

namespace DailyPlanner.Dto
{
    public abstract class EntityDto
    {
        public Guid Id { get; set; }

        public DateTimeOffset CreatedAt { get; set; }
    }
}
