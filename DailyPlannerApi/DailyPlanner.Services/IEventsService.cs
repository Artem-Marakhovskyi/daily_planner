using DailyPlanner.Dto.Calendars;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DailyPlanner.Services
{
    public interface IEventsService
    {
        Task<IEnumerable<EventDto>> GetAsync(Guid userId);
        Task RemoveAsync(IEnumerable<Guid> eventIds);
        Task<IEnumerable<EventDto>> UpsertAsync(IEnumerable<EventDto> eventDtos);
    }
}