using DailyPlanner.Entities;
using DailyPlanner.Entities.Calendar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DailyPlanner.Console.Generators
{
    class EventSharingGenerator
    {
        private List<EventSharing> _eventSharings;
        private Random _random;

        public EventSharingGenerator()
        {
            _random = new Random();
        }

        internal List<EventSharing> Generate(List<Person> people, List<Event> events, int count)
        {
            _eventSharings = new List<EventSharing>();

            for(var i = 0; i < count; i++)
            {
                var creatorId = people[_random.Next(people.Count)].Id;
                var receiverId = people[_random.Next(people.Count)].Id;
                while (creatorId == receiverId)
                {
                    receiverId = people[_random.Next(people.Count)].Id;
                }

                var sharing = new EventSharing()
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.Now,
                    EventId = events[_random.Next(events.Count)].Id,
                    CreatorId = creatorId,
                    IsReceiverRequired = _random.Next(2) == 0 ? true : false,
                    ReceiverId = receiverId
                };

                if (_eventSharings.Any(e => e.EventId == sharing.EventId && e.CreatorId == sharing.CreatorId && e.ReceiverId == sharing.ReceiverId))
                {
                    continue;
                }

                _eventSharings.Add(sharing);

                if (i % 1000 == 0)
                {
                    System.Console.WriteLine($"{(Convert.ToDouble(i) * 100)/count}% of EventSharing is done");
                }
            }

            return _eventSharings;
        }
    }
}
