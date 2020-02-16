using System;
namespace DailyPlanner.Entities
{
    public abstract class Entity
    {
        public Guid Id { get; set; }

        public DateTimeOffset CreatedAt { get; set; }
    }
}
