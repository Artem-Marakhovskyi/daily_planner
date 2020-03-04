using DailyPlanner.Api.ViewDtos;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace DailyPlanner.Api.Validation
{
    public static class ValidationServicesStartup
    {
        public static void Startup(IServiceCollection services)
        {
            services.AddSingleton<IValidator<PersonRegisterDto>, RegisterPersonDtoValidator>();
        }
    }
}
