using AutoMapper;
using DailyPlanner.Services;

namespace DailyPlanner.Api
{
    public class MapperStartup
    {
        public MapperConfiguration GetConfiguration()
        {
            return new MapperConfiguration(
                mc =>
                {
                    mc.AddProfile(new ServicesMappingProfile());
                });
        }
    }
}
