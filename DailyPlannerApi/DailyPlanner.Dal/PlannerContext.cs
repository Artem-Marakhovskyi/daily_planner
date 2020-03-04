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

        // PERSON
        public DbSet<Person> People { get; set; }

        
        // NOTES
        public DbSet<Note> Notes { get; set; }


        // REMINDER
        public DbSet<Reminder> Reminders { get; set; }

        public DbSet<ReminderSharing> ReminderSharings { get; set; }

        
        // EVENTS
        public DbSet<Event> Events { get; set; }

        public DbSet<EventSharing> EventSharings { get; set; }

        
        // TAG
        public DbSet<Tag> Tags { get; set; }

        
        // CHAT
        public DbSet<Chatroom> Chatrooms { get; set; }

        public DbSet<ChatroomParticipation> ChatroomParticipants { get; set; }

        public DbSet<Message> Messages { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            ConfigureNotes(modelBuilder);

            ConfigureReminders(modelBuilder);

            ConfigureTagRelations(modelBuilder);

            ConfigureEvents(modelBuilder);

            ConfigureChat(modelBuilder);
        }

        private void ConfigureChat(ModelBuilder modelBuilder)
        {
            // chatroom particapation - person
            modelBuilder.Entity<ChatroomParticipation>()
                .HasOne(cp => cp.Participant)
                .WithMany(p => p.ChatroomParticipations);

            // chatroom participation - chatroom
            modelBuilder.Entity<ChatroomParticipation>()
                .HasOne(cp => cp.Chatroom)
                .WithMany(c => c.ChatroomParticipants);

            // message - person
            modelBuilder.Entity<Message>()
                .HasOne(m => m.Sender);

            modelBuilder.Entity<Message>()
                .HasOne(m => m.Chatroom)
                .WithMany(c => c.Messages);
        }

        private void ConfigureEvents(ModelBuilder modelBuilder)
        {
            // event - person
            modelBuilder.Entity<Event>()
                .HasOne(e => e.Creator)
                .WithMany(p => p.Events);

            // event - event-sharing
            modelBuilder.Entity<Event>()
                .HasMany(e => e.EventSharings)
                .WithOne(es => es.Event)
                .OnDelete(DeleteBehavior.NoAction);

            // event-sharing - person (creator)
            modelBuilder.Entity<EventSharing>()
                .HasOne(es => es.Creator);

            // event-sharing - person (receiver)
            modelBuilder.Entity<Person>()
                .HasMany(p => p.EventsReceived)
                .WithOne(ev => ev.Receiver)
                .OnDelete(DeleteBehavior.NoAction);
        }

        private void ConfigureReminders(ModelBuilder modelBuilder)
        {
            // reminder - reminder-sharing
            modelBuilder.Entity<Reminder>()
                .HasMany(r => r.ReminderSharings)
                .WithOne(rm => rm.Reminder);

            // reminder - person
            modelBuilder.Entity<Reminder>()
                .HasOne(r => r.Creator)
                .WithMany(c => c.Reminders);

            // reminder-sharing - person (creator)
            modelBuilder.Entity<ReminderSharing>()
                .HasOne(rm => rm.Sender)
                .WithMany(p => p.RemindersSent)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ReminderSharing>()
                .HasOne(rm => rm.Receiver)
                .WithMany(p => p.RemindersReceived)
                .OnDelete(DeleteBehavior.NoAction);
        }

        private void ConfigureNotes(ModelBuilder modelBuilder)
        {
            // person - note
            modelBuilder.Entity<Note>()
                .HasOne(n => n.Creator)
                .WithMany(p => p.Notes);
        }

        private void ConfigureTagRelations(ModelBuilder modelBuilder)
        {
            // note - tag
            modelBuilder.Entity<Note>()
                .HasOne(n => n.Tag);

            // event - tag
            modelBuilder.Entity<Event>()
                .HasOne(e => e.Tag);

            // reminder - tag
            modelBuilder.Entity<Reminder>()
                .HasOne(r => r.Tag);

            // message - tag
            modelBuilder.Entity<Message>()
                .HasOne(m => m.Tag);
        }
    }
}
