using DailyPlanner.Console.Generators.SimpleGenerators;
using DailyPlanner.Entities.Chat;
using System;
using System.Collections.Generic;
using System.Text;

namespace DailyPlanner.Console.Generators
{
    class ChatroomGenerator
    {
        private TextGenerator _textGenerator;
        private Random _random;
        private List<Chatroom> _chatrooms;

        public ChatroomGenerator(TextGenerator textGenerator)
        {
            _textGenerator = textGenerator;
            _random = new Random();
        }

        internal List<Chatroom> Generate(int count)
        {
            _chatrooms = new List<Chatroom>();

            for (int i = 0; i < count; i++)
            {
                _chatrooms.Add(
                    new Chatroom()
                    {
                        CreatedAt = DateTime.Now,
                        Id = Guid.NewGuid(),
                        Title = _textGenerator.GetWord()
                    });
            }

            return _chatrooms;
        }
    }
}
