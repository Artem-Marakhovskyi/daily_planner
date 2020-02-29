using DailyPlanner.Console.Generators;
using DailyPlanner.Dal;
using DailyPlanner.Entities;
using DailyPlanner.Entities.Calendar;
using DailyPlanner.Entities.Chat;
using DailyPlanner.Entities.Notes;
using DailyPlanner.Entities.Reminders;
using DailyPlanner.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Threading.Tasks;

namespace DailyPlanner.Console
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var read = System.Console.ReadLine();

            if (read == "gen")
            {
                Generate();
            }
            else if (read == "ptest")
            {
                await PersonTest();
            }
        }

        private static async Task PersonTest()
        {
            using (var context = new PlannerContext(GetContextOptionsBuilder().Options))
            {
                var first = await context.Set<Person>().FirstAsync();
                var passwordManager = new PasswordHashManager();
                System.Console.WriteLine(
                    passwordManager.IsPasswordValid(first.FirstName.ToLower(), first.PasswordHash, first.Salt));
            }
        }

        private static void Generate()
        {
            var generator = new DbDataGenerator(File.ReadAllText("LoremIpsum.txt"), File.ReadAllLines("Names.txt"));
            var data = generator.GenerateDb();

            try
            {

                using (var plannerContext = new PlannerContext(GetContextOptionsBuilder().Options))
                {
                    plannerContext.Database.EnsureCreated();

                    plannerContext.Set<Tag>().AddRange(data.Tags);
                    plannerContext.Set<Person>().AddRange(data.People);
                    plannerContext.Set<Note>().AddRange(data.Notes);
                    plannerContext.Set<Event>().AddRange(data.Events);
                    plannerContext.Set<EventSharing>().AddRange(data.EventSharings);
                    plannerContext.Set<Reminder>().AddRange(data.Reminders);
                    plannerContext.Set<ReminderSharing>().AddRange(data.ReminderSharings);
                    plannerContext.Set<Chatroom>().AddRange(data.Chatrooms);
                    plannerContext.Set<ChatroomParticipation>().AddRange(data.ChatroomParticipants);
                    plannerContext.Set<Message>().AddRange(data.Messages);

                    plannerContext.SaveChanges();
                }

            }
            catch (Exception e)
            {
                System.Console.WriteLine(e);
                throw;
            }
        }

        private static DbContextOptionsBuilder GetContextOptionsBuilder()
        {
            return new DbContextOptionsBuilder()
                .UseSqlServer("Data Source=localhost\\SQLEXPRESS;Initial Catalog=PlannerDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

        }
    }
}
