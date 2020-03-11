using DailyPlanner.Dal;
using DailyPlanner.Entities;
using DailyPlanner.Entities.Calendar;
using DailyPlanner.Entities.Chat;
using DailyPlanner.Entities.Notes;
using DailyPlanner.Entities.Reminders;
using DailyPlanner.Infrastructure;
using DailyPlanner.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DailyPlanner.Ioc
{
    public class IocRegistration
    {
        public static void Register(IServiceCollection serviceCollection, IConfiguration configuration)
        {
            RegisterServices(serviceCollection);

            RegisterRepositories(serviceCollection);

            RegisterDbContext(serviceCollection, configuration);
        }

        private static void RegisterDbContext(IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection
                .AddEntityFrameworkSqlServer()
                .AddDbContext<PlannerContext>(
                    op => op.UseSqlServer(configuration.GetConnectionString("remote")),
                    contextLifetime: ServiceLifetime.Scoped);
        }

        private static void RegisterRepositories(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IRepository<Tag>, Repository<Tag>>();
            serviceCollection.AddScoped<IRepository<Note>, Repository<Note>>();
            serviceCollection.AddScoped<IRepository<Person>, Repository<Person>>();
            serviceCollection.AddScoped<IRepository<Event>, Repository<Event>>();
            serviceCollection.AddScoped<IRepository<EventSharing>, Repository<EventSharing>>();
            serviceCollection.AddScoped<IRepository<Reminder>, Repository<Reminder>>();
            serviceCollection.AddScoped<IRepository<ReminderSharing>, Repository<ReminderSharing>>();
            serviceCollection.AddScoped<IRepository<Chatroom>, Repository<Chatroom>>();
            serviceCollection.AddScoped<IRepository<ChatroomParticipation>, Repository<ChatroomParticipation>>();
            serviceCollection.AddScoped<IRepository<Message>, Repository<Message>>();

            serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        private static void RegisterServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<INotesService, NotesService>();
            serviceCollection.AddScoped<IPeopleService, PeopleService>();
            serviceCollection.AddScoped<IEventsService, EventsService>();
            serviceCollection.AddScoped<IRemindersService, RemindersService>();
            serviceCollection.AddScoped<IChatService, ChatService>();
            serviceCollection.AddScoped<IMessagesService, MessagesService>();

            serviceCollection.AddScoped<IPasswordHashManager, PasswordHashManager>();
        }
    }
}
