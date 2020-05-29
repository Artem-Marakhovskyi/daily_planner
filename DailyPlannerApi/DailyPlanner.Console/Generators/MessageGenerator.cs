using DailyPlanner.Console.Generators.SimpleGenerators;
using DailyPlanner.Entities;
using DailyPlanner.Entities.Chat;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DailyPlanner.Console.Generators
{
    class MessageGenerator
    {
        private TextGenerator _textGenerator;
        private Random _random;
        private List<Message> _messages;

        public MessageGenerator(TextGenerator textGenerator)
        {
            _textGenerator = textGenerator;
            _random = new Random();
        }

        internal List<Message> Generate(List<ChatroomParticipation> participation, List<Chatroom> chatrooms, List<Tag> tags, int count)
        {
            _messages = new List<Message>();


            foreach (var chatroom in chatrooms)
            {
                var chatParticipants = participation.Where(e => e.ChatroomId == chatroom.Id).ToList();


                var dateSent = DateTime.Now.Subtract(TimeSpan.FromDays(_random.NextDouble() * 10));
                var dateReceived = dateSent + TimeSpan.FromMinutes(2);
                var dateRead = dateReceived + TimeSpan.FromHours(_random.NextDouble() * 24);

                var message = new Message
                {
                    Id = Guid.NewGuid(),
                    ChartoomId = chatroom.Id,
                    CreatedAt = DateTime.Now,
                    TagId = tags[_random.Next(tags.Count)].Id,
                    SenderId = _random.Next(10) > 4 ? chatParticipants.First().ParticipantId : chatParticipants.Last().ParticipantId,
                    Text = string.Join(" ", Enumerable.Range(0, _random.Next(5))
                           .Select(e => _textGenerator.GetSentence())),
                    DateReceived = dateReceived,
                    ReferenceId = null,
                    DateSent = dateSent,
                    DateRead = dateRead,
                    Type = MessageType.Text
                };

                if (string.IsNullOrEmpty(message.Text))
                {
                    message.Text = "hello";
                }

                _messages.Add(message);
            }

            return _messages;
        }
    }
}
