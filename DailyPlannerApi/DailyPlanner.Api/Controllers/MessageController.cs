using DailyPlanner.Dto.Chat;
using DailyPlanner.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DailyPlanner.Api.Controllers
{
    [Route("[controller]")]
    [Authorize]
    [ApiController]
    public class MessageController
    {
        private readonly IMessagesService _messagesService;
        private readonly ILogger<MessageController> _logger;

        public MessageController(
            IMessagesService messagesService,
            ILogger<MessageController> logger)
        {
            _messagesService = messagesService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IEnumerable<MessageDto>> Post(IEnumerable<MessageDto> messageDtos)
        {
            var message = await _messagesService.UpsertAsync(messageDtos);

            return message;
        }

        [HttpGet]
        public async Task<IEnumerable<MessageDto>> Get(Guid chatroomId)
        {
            var messages = await _messagesService.GetAsync(chatroomId);

            return messages;
        }
    }
}
