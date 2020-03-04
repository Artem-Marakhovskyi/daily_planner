using AutoMapper;
using DailyPlanner.Api.ViewDtos;

namespace DailyPlanner.Api
{
    internal class ApiMappingProfile : Profile
    {
        public ApiMappingProfile()
        {
            CreateMap<PersonRegisterDto, PersonDto>();
        }
    }
}