using DailyPlanner.Dto.Chat;
using DailyPlanner.Dto.Chatroom;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DailyPlanner.Services
{
    public interface IChatService
    {
        Task<IEnumerable<ChatroomDto>> GetAsync(Guid participantId);

        Task<IEnumerable<ChatroomDto>> UpsertAsync(IEnumerable<ChatroomDto> chatroomsDtos);

        Task<List<ChatroomParticipationDto>> AddParticipantsAsync(IEnumerable<ChatroomParticipationDto> chatroomParticipationDtos);
    }
}
