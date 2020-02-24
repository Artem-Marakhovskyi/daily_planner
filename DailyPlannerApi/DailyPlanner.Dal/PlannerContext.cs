using DailyPlanner.Entities;
using DailyPlanner.Entities.Calendar;
using DailyPlanner.Entities.Chat;
using DailyPlanner.Entities.Notes;
using DailyPlanner.Entities.Reminders;
using Microsoft.EntityFrameworkCore;

namespace DailyPlanner.Dal
{
    public class PlannerContext : DbContext
    {
        public PlannerContext(DbContextOptions options) : base(options)
        {
                
        }

        public DbSet<Person> People { get; set; }

        public DbSet<Note> Notes { get; set; }

        public DbSet<Reminder> Reminders { get; set; }

        public DbSet<ReminderSharing> ReminderSharings { get; set; }

        public DbSet<Event> Events { get; set; }

        public DbSet<EventSharing> EventSharings { get; set; }

        public DbSet<Tag> Tags { get; set; }

        public DbSet<Chatroom> Chatrooms { get; set; }

        public DbSet<ChatroomParticipant> ChatroomParticipants { get; set; }

        public DbSet<Message> Messages { get; set; }
    }
}
