using DailyPlanner.Dal;
using DailyPlanner.Entities;
using DailyPlanner.Entities.Notes;
using DailyPlanner.Infrastructure;
using DailyPlanner.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

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
                    op => op.UseSqlServer(configuration.GetConnectionString("local")),
                    contextLifetime: ServiceLifetime.Scoped);
        }

        private static void RegisterRepositories(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IRepository<Tag>, Repository<Tag>>();
            serviceCollection.AddScoped<IRepository<Note>, Repository<Note>>();
            serviceCollection.AddScoped<IRepository<Person>, Repository<Person>>();

            serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        private static void RegisterServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<INotesService, NotesService>();
            serviceCollection.AddScoped<IPersonService, PersonService>();
            serviceCollection.AddScoped<IPasswordHashManager, PasswordHashManager>();
        }
    }
}
