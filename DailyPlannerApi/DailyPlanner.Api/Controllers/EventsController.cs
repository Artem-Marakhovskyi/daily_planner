using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DailyPlanner.Dto.Calendars;
using DailyPlanner.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DailyPlanner.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class EventsController : ControllerBase
    {               
        private readonly ILogger<EventsController> _logger;
        private readonly IEventsService _eventsService;

        public EventsController(
            IEventsService eventsService,
            ILogger<EventsController> logger)
        {
            _logger = logger;
            _eventsService = eventsService;
        }

        [HttpGet]
        public async Task<IEnumerable<EventDto>> Get()
        {
            var events = await _eventsService.GetAsync(HttpContext.User.GetId().Value);

            return events;
        }

        [HttpDelete]
        public async Task<StatusCodeResult> Delete(Guid[] eventIds)
        {
            await _eventsService.RemoveAsync(eventIds);

            return Ok();
        }

        [HttpPost]
        public async Task<object> Post(IEnumerable<EventDto> eventDtos)
        {
            var newEvents = await _eventsService.UpsertAsync(eventDtos);

            return Ok(newEvents);
        }
    }
}
