using DailyPlanner.Api.ViewDtos;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
