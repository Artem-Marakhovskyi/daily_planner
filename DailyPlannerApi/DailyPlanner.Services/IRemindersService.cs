using DailyPlanner.Dto.Reminders;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DailyPlanner.Services
{
    public interface IRemindersService
    {
        Task<IEnumerable<ReminderDto>> GetAsync(Guid userId);
        Task RemoveAsync(IEnumerable<Guid> reminderIds);
        Task<IEnumerable<ReminderDto>> UpsertAsync(IEnumerable<ReminderDto> reminderDtos);
    }
}