using DailyPlanner.Console.Generators.SimpleGenerators;
using DailyPlanner.Infrastructure;

namespace DailyPlanner.Console.Generators
{
    public class DbDataGenerator
    {
        private readonly TextGenerator _textGenerator;

        private ChatroomGenerator _chatroomGenerator;
        private ChatroomParticipationGenerator _chatroomParticipationGenerator;
        private EventGenerator _eventGenerator;
        private EventSharingGenerator _eventSharingGenerator;
        private MessageGenerator _messageGenerator;
        private NoteGenerator _noteGenerator;
        private PersonGenerator _personGenerator;
        private ReminderGenerator _reminderGenerator;
        private ReminderSharingGenerator _reminderSharingGenerator;
        private TagsGenerator _tagsGenerator;

        public DbDataGenerator(
            string textSample, 
            string[] namesSample)
        {
            _textGenerator = new TextGenerator(textSample);

            _tagsGenerator = new TagsGenerator();
            
            _personGenerator = new PersonGenerator(new PasswordHashManager(), namesSample, _textGenerator);
            
            _noteGenerator = new NoteGenerator(_textGenerator);

            _eventGenerator = new EventGenerator(_textGenerator);
            _eventSharingGenerator = new EventSharingGenerator();

            _reminderGenerator = new ReminderGenerator(_textGenerator);
            _reminderSharingGenerator = new ReminderSharingGenerator();

            _chatroomGenerator = new ChatroomGenerator(_textGenerator);
            _chatroomParticipationGenerator = new ChatroomParticipationGenerator();
            _messageGenerator = new MessageGenerator(_textGenerator);
        }

        public DbData GenerateDb()
        {
            System.Console.WriteLine("GENERATION STARTED");
            var people = _personGenerator.Generate();
            System.Console.WriteLine("PEOPLE generated. 1/10");
            var tags = _tagsGenerator.Generate();
            System.Console.WriteLine("TAGS generated. 2/10");
            var notes = _noteGenerator.Generate(people, tags, 1000);
            System.Console.WriteLine("NOTES generated. 3/10");
            var events = _eventGenerator.Generate(people, tags, 10000);
            System.Console.WriteLine("EVENT generated. 4/10");
            var eventSharings = _eventSharingGenerator.Generate(people, events, 20000);
            System.Console.WriteLine("EVENT SHARING generated. 5/10");
            var reminders = _reminderGenerator.Generate(people, tags, 2000);
            System.Console.WriteLine("REMINDER generated. 6/10");
            var reminderSharings = _reminderSharingGenerator.Generate(people, reminders, 2000);
            System.Console.WriteLine("REMINDER SHARING generated. 7/10");
            var chatrooms = _chatroomGenerator.Generate(people.Count * 8);
            System.Console.WriteLine("CHATROOM generated. 8/10");
            var chatroomParticipation = _chatroomParticipationGenerator.Generate(people, chatrooms, chatrooms.Count * 2);
            System.Console.WriteLine("CHATROOM PARTICIPANTS generated. 9/10");
            var messages = _messageGenerator.Generate(people, chatrooms, tags, 50000);
            System.Console.WriteLine("MESSAGES generated. 10/10");

            return new DbData
            {
                People = people,
                Tags = tags,
                Notes = notes,
                Reminders = reminders,
                ReminderSharings = reminderSharings,
                Events = events,
                EventSharings = eventSharings,
                ChatroomParticipants = chatroomParticipation,
                Chatrooms = chatrooms,
                Messages = messages
            };
        }
    }
}
