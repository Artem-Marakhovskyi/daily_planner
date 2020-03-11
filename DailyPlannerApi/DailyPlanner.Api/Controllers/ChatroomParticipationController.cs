using DailyPlanner.Dto.Chat;
using DailyPlanner.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DailyPlanner.Api.Controllers
{
    [Route("[controller]")]
    [Authorize]
    [ApiController]
    public class ChatroomParticipationController : ControllerBase
    {
        private readonly IChatService _chatService;
        private ILogger<ChatController> _logger;

        public ChatroomParticipationController(
            IChatService chatService,
            ILogger<ChatController> logger)
        {
            _chatService = chatService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IEnumerable<ChatroomParticipationDto>> Post(IEnumerable<ChatroomParticipationDto> newParticipations)
        {
            var resultingParticipations = await _chatService.AddParticipantsAsync(newParticipations);

            return resultingParticipations;
        }
    }
}