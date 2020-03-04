using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DailyPlanner.Dto.Notes;
using DailyPlanner.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DailyPlanner.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class NotesController : ControllerBase
    {               
        private readonly ILogger<NotesController> _logger;
        private readonly INotesService _notesService;

        public NotesController(
            INotesService notesService,
            ILogger<NotesController> logger)
        {
            _logger = logger;
            _notesService = notesService;
        }

        [HttpGet]
        public async Task<IEnumerable<NoteDto>> Get()
        {
            
            var notes = await _notesService.GetAsync(HttpContext.User.GetId().Value);

            return notes;
        }

        [HttpDelete]
        public async Task<StatusCodeResult> Delete(Guid[] noteIds)
        {
            await _notesService.RemoveAsync(noteIds);

            return Ok();
        }

        [HttpPost]
        public async Task<StatusCodeResult> Post(IEnumerable<NoteDto> noteDtos)
        {
            await _notesService.UpsertAsync(noteDtos);

            return Ok();
        }
    }
}
