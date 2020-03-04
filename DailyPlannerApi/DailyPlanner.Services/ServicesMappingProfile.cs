using AutoMapper;
using DailyPlanner.Dto;
using DailyPlanner.Dto.Notes;
using DailyPlanner.Entities;
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
            CreateMap<Note, NoteDto>().ReverseMap()
                .ForMember(s => s.Tag, opt => opt.Ignore());
            CreateMap<Person, PersonDto>().ReverseMap();
        }
    }
}
