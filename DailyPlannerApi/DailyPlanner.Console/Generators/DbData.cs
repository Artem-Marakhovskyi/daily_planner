using DailyPlanner.Entities;
using DailyPlanner.Entities.Calendar;
using DailyPlanner.Entities.Chat;
using DailyPlanner.Entities.Notes;
using DailyPlanner.Entities.Reminders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DailyPlanner.Console.Generators
{
    public class DbData
    {
        // PERSON
        public List<Person> People { get; set; }


        // NOTES
        public List<Note> Notes { get; set; }


        // REMINDER
        public List<Reminder> Reminders { get; set; }

        public List<ReminderSharing> ReminderSharings { get; set; }


        // EVENTS
        public List<Event> Events { get; set; }

        public List<EventSharing> EventSharings { get; set; }


        // TAG
        public List<Tag> Tags { get; set; }


        // CHAT
        public List<Chatroom> Chatrooms { get; set; }

        public List<ChatroomParticipation> ChatroomParticipants { get; set; }

        public List<Message> Messages { get; set; }

    }
}
