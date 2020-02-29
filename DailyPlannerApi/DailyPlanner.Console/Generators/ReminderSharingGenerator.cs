using DailyPlanner.Entities;
using DailyPlanner.Entities.Reminders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DailyPlanner.Console.Generators
{
    class ReminderSharingGenerator
    {
        private List<ReminderSharing> _reminderSharings;
        private Random _random;

        public ReminderSharingGenerator()
        {
            _random = new Random();
        }

        internal List<ReminderSharing> Generate(List<Person> people, List<Reminder> reminders, int count)
        {
            _reminderSharings = new List<ReminderSharing>();

            for (int i = 0; i < count; i++)
            {
                var sharing = new ReminderSharing()
                {
                    Id = Guid.NewGuid(),
                    ReminderId = reminders[_random.Next(reminders.Count)].Id,
                    CreatedAt = DateTime.Now,
                    ReceiverId = people[_random.Next(people.Count)].Id,
                    SenderId = people[_random.Next(people.Count)].Id,
                };

                if (_reminderSharings.Any(e => (e.ReceiverId == sharing.ReceiverId &&
                    e.SenderId == sharing.SenderId && e.ReminderId == sharing.ReminderId)
                    || (sharing.ReminderId == sharing.SenderId)))
                {
                    continue;
                }

                _reminderSharings.Add(sharing);
            }

            return _reminderSharings;
        }
    }
}
