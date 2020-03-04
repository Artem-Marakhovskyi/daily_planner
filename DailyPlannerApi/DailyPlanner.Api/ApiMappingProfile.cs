using AutoMapper;
using DailyPlanner.Api.ViewDtos;
using DailyPlanner.Dto.Notes;
using System;

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