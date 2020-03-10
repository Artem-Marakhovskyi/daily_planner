using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DailyPlanner.Dto.Calendars;
using DailyPlanner.Dto.Reminders;
using DailyPlanner.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DailyPlanner.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class RemindersController : ControllerBase
    {
        private readonly ILogger<RemindersController> _logger;
        private readonly IRemindersService _remindersService;

        public RemindersController(
            IRemindersService remindersService,
            ILogger<RemindersController> logger)
        {
            _logger = logger;
            _remindersService = remindersService;
        }

        [HttpGet]
        public async Task<IEnumerable<ReminderDto>> Get()
        {
            var reminders = await _remindersService.GetAsync(HttpContext.User.GetId().Value);

            return reminders;
        }

        [HttpDelete]
        public async Task<StatusCodeResult> Delete(Guid[] reminderIds)
        {
            await _remindersService.RemoveAsync(reminderIds);

            return Ok();
        }

        [HttpPost]
        public async Task<object> Post(IEnumerable<ReminderDto> reminderDtos)
        {
            var newReminders = await _remindersService.UpsertAsync(reminderDtos);

            return Ok(newReminders);
        }
    }
}
