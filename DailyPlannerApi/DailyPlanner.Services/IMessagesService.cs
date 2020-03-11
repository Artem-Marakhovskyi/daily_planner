using DailyPlanner.Dto.Chat;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DailyPlanner.Services
{
    public interface IMessagesService
    {
        Task<IEnumerable<MessageDto>> GetAsync(Guid userId);
        Task<IEnumerable<MessageDto>> UpsertAsync(IEnumerable<MessageDto> messageDtos);
    }
}