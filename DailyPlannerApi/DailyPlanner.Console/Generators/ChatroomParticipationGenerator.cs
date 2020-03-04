using DailyPlanner.Entities;
using DailyPlanner.Entities.Chat;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DailyPlanner.Console.Generators
{
    class ChatroomParticipationGenerator
    {
        private Random _random;
        private List<ChatroomParticipation> _chatroomParticipations;

        public ChatroomParticipationGenerator()
        {
            _random = new Random();
        }

        internal List<ChatroomParticipation> Generate(List<Person> people, List<Chatroom> chatrooms, int count)
        {
            _chatroomParticipations = new List<ChatroomParticipation>();

            for (int i = 0; i < count; i++)
            {
                var participation = new ChatroomParticipation
                {
                    Id = Guid.NewGuid(),
                    ChatroomId = chatrooms[_random.Next(chatrooms.Count)].Id,
                    CreatedAt = DateTime.Now,
                    ParticipantId = people[_random.Next(people.Count)].Id
                };

                while (_chatroomParticipations.Any(c => c.ParticipantId == participation.ParticipantId
                    && c.ChatroomId == participation.ChatroomId))
                {
                    participation.ParticipantId = people[_random.Next(people.Count)].Id;
                }

                _chatroomParticipations.Add(participation);
            }

            List<Guid> excludeGuid = new List<Guid>();
            foreach (var item in _chatroomParticipations)
            {
                if (_chatroomParticipations.Where(e => e.ChatroomId == item.Id).Count() == 1)
                {
                    excludeGuid.Add(item.Id);
                }
            }

            return _chatroomParticipations.Where(e => !excludeGuid.Contains(e.Id)).ToList();
        }
    }
}
