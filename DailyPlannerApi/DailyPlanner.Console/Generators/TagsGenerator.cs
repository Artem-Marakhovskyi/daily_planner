using DailyPlanner.Entities;
using System;
using System.Collections.Generic;

namespace DailyPlanner.Console.Generators
{
    class TagsGenerator
    {
        public List<Tag> Generate()
        {
            return new List<Tag>()
            {
                new Tag() { CreatedAt = DateTime.Now, Description = "2389ff", Id = Guid.NewGuid() },
                new Tag() { CreatedAt = DateTime.Now, Description = "ff87ed", Id = Guid.NewGuid() },
                new Tag() { CreatedAt = DateTime.Now, Description = "1298ac", Id = Guid.NewGuid() },
                new Tag() { CreatedAt = DateTime.Now, Description = "987451", Id = Guid.NewGuid() },
                new Tag() { CreatedAt = DateTime.Now, Description = "ffff22", Id = Guid.NewGuid() },
                new Tag() { CreatedAt = DateTime.Now, Description = "12ffff", Id = Guid.NewGuid() },
                new Tag() { CreatedAt = DateTime.Now, Description = "98f901", Id = Guid.NewGuid() },
            };
        }
    }
}
