using AutoMapper;
using DailyPlanner.Api.ViewDtos;
using DailyPlanner.Dto;

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