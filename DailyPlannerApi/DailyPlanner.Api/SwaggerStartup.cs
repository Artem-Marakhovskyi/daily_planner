using Microsoft.AspNetCore.Builder;

namespace DailyPlanner.Api
{
    public static class SwaggerStartup
    {
        public static void Configure(IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Daily planner V1");
                c.RoutePrefix = string.Empty;
            });


        }
    }
}
