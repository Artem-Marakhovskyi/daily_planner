using DailyPlanner.Dto.Notes;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DailyPlanner.Services
{
    public interface INotesService
    {
        Task<IEnumerable<NoteDto>> GetAsync(Guid creatorId);

        Task<IEnumerable<NoteDto>> UpsertAsync(IEnumerable<NoteDto> noteDto);

        Task RemoveAsync(IEnumerable<Guid> noteIds);
    }
}
