using DailyPlanner.Console.Generators.SimpleGenerators;
using DailyPlanner.Entities;
using DailyPlanner.Entities.Reminders;
using System;
using System.Collections.Generic;

namespace DailyPlanner.Console.Generators
{
    class ReminderGenerator
    {
        private TextGenerator _textGenerator;
        private Random _random;
        private List<Reminder> _reminders;

        public ReminderGenerator(TextGenerator textGenerator)
        {
            _textGenerator = textGenerator;
            _random = new Random();
        }

        internal List<Reminder> Generate(List<Person> people, List<Tag> tags, int count)
        {
            _reminders = new List<Reminder>();

            for (var i = 0; i < count; i++)
            {
                _reminders.Add(new Reminder()
                {
                    CreatedAt = DateTime.Now,
                    CreatorId = people[_random.Next(people.Count)].Id,
                    Id = Guid.NewGuid(),
                    TagId = tags[_random.Next(tags.Count)].Id,
                    Title = _textGenerator.GetWord(),
                    Description = _textGenerator.GetSentence()
                });
            }

            return _reminders;
        }
    }
}
