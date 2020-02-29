using DailyPlanner.Console.Generators.SimpleGenerators;
using DailyPlanner.Entities;
using DailyPlanner.Entities.Notes;
using System;
using System.Collections.Generic;

namespace DailyPlanner.Console.Generators
{
    class NoteGenerator
    {
        private readonly TextGenerator _textGenerator;
        private readonly Random _random;
        private List<Note> _notes = new List<Note>();

        public NoteGenerator(TextGenerator textGenerator)
        {
            _textGenerator = textGenerator;
            _random = new Random();
        }

        internal List<Note> Generate(List<Person> people, List<Tag> tags, int count)
        {
            for (var i = 0; i < count; i++)
            {
                _notes.Add(new Note
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.Now,
                    Description = _textGenerator.GetSentence() + " " + _textGenerator.GetSentence(),
                    Title = _textGenerator.GetWord(),
                    CreatorId = people[_random.Next(people.Count)].Id,
                    TagId = tags[_random.Next(tags.Count)].Id
                });
            }

            return _notes;
        }
    }
}
