using AutoMapper;
using DailyPlanner.Dto.Notes;
using DailyPlanner.Entities.Notes;

namespace DailyPlanner.Services
{
    public class ServicesMappingProfile : Profile
    {
        public ServicesMappingProfile()
        {
            CreateEntitiesMaps();
        }

        private void CreateEntitiesMaps()
        {
            CreateMap<Note, NoteDto>().ReverseMap();
        }
    }
}
