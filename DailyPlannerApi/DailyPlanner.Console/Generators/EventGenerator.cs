using DailyPlanner.Console.Generators.SimpleGenerators;
using DailyPlanner.Entities;
using DailyPlanner.Entities.Calendar;
using System;
using System.Collections.Generic;
using System.Text;

namespace DailyPlanner.Console.Generators
{
    class EventGenerator
    {
        private TextGenerator _textGenerator;
        private readonly Random _random;
        private List<Event> _events;

        public EventGenerator(TextGenerator textGenerator)
        {
            _textGenerator = textGenerator;
            _random = new Random();
        }

        public List<Event> Generate(List<Person> people, List<Tag> tags, int count)
        {
            _events = new List<Event>();

            for (var i = 0; i < count; i++)
            {
                var startTime = _random.Next(10) > 3
                    ? DateTime.Now.Add(TimeSpan.FromDays(_random.Next(500)))
                    : DateTime.Now.Subtract(TimeSpan.FromDays(_random.Next(4)));

                var endTime = _random.Next(10) > 7
                    ? startTime.Add(TimeSpan.FromDays(_random.NextDouble() * 3))
                    : startTime.Add(TimeSpan.FromHours(_random.NextDouble() * 2));

                _events.Add(
                    new Event
                    {
                        CreatorId = people[_random.Next(people.Count)].Id,
                        Description = _textGenerator.GetSentence(),
                        Id = Guid.NewGuid(),
                        TagId = tags[_random.Next(tags.Count)].Id,
                        Title = _textGenerator.GetWord(),
                        StartTime = startTime,
                        EndTime = endTime,
                        CreatedAt = DateTime.Now
                    });
            }

            return _events;
        }
    }
}
