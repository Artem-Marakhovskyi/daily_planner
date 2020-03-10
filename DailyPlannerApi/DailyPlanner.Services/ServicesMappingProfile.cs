using AutoMapper;
using DailyPlanner.Dto;
using DailyPlanner.Dto.Calendar;
using DailyPlanner.Dto.Calendars;
using DailyPlanner.Dto.Notes;
using DailyPlanner.Dto.Reminders;
using DailyPlanner.Entities;
using DailyPlanner.Entities.Calendar;
using DailyPlanner.Entities.Notes;
using DailyPlanner.Entities.Reminders;

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
           
            
            CreateMap<Event, EventDto>().ReverseMap()
                .ForMember(s => s.Tag, opt => opt.Ignore());
            CreateMap<EventSharing, EventSharingDto>().ReverseMap();
            
            
            CreateMap<Reminder, ReminderDto>()
                .ForMember(s => s.Tag, opt => opt.Ignore());
            CreateMap<EventSharingDto, EventSharing>().ReverseMap();
        }
    }
}
