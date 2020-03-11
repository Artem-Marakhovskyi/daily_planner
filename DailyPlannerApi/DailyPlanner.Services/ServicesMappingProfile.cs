using AutoMapper;
using DailyPlanner.Dto;
using DailyPlanner.Dto.Calendar;
using DailyPlanner.Dto.Calendars;
using DailyPlanner.Dto.Chat;
using DailyPlanner.Dto.Chatroom;
using DailyPlanner.Dto.Notes;
using DailyPlanner.Dto.Reminders;
using DailyPlanner.Entities;
using DailyPlanner.Entities.Calendar;
using DailyPlanner.Entities.Chat;
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
            
            
            CreateMap<Reminder, ReminderDto>().ReverseMap()
                .ForMember(s => s.Tag, opt => opt.Ignore());

            CreateMap<ReminderSharingDto, ReminderSharing>().ReverseMap();

            CreateMap<Chatroom, ChatroomDto>().ReverseMap();
            CreateMap<ChatroomParticipation, ChatroomParticipationDto>().ReverseMap();
            CreateMap<Message, MessageDto>()
                .ForMember(m => m.ChatoomId, opt => opt.MapFrom(entity => entity.ChartoomId))
                .ReverseMap()
                .ForMember(m => m.Tag, opt => opt.Ignore())
                .ForMember(m => m.ChartoomId, opt => opt.MapFrom(dto => dto.ChatoomId));

            CreateMap<MessageType, MessageDtoType>().ReverseMap();
        }
    }
}
