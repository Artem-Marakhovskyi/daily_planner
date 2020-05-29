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

            foreach (var chatroom in chatrooms)
            {
                var firstP = people[_random.Next(people.Count)];
                var secondP = people[_random.Next(people.Count)];
                while (firstP.Id == secondP.Id)
                {
                    secondP = people[_random.Next(people.Count)];
                }

                var participation1 = new ChatroomParticipation
                {
                    Id = Guid.NewGuid(),
                    ChatroomId = chatroom.Id,
                    CreatedAt = DateTime.Now,
                    ParticipantId = firstP.Id
                };

                var participation2 = new ChatroomParticipation
                {
                    Id = Guid.NewGuid(),
                    ChatroomId = chatroom.Id,
                    CreatedAt = DateTime.Now,
                    ParticipantId = secondP.Id
                };

                _chatroomParticipations.Add(participation1);
                _chatroomParticipations.Add(participation2);
            }

            return _chatroomParticipations.ToList();
        }
    }
}
