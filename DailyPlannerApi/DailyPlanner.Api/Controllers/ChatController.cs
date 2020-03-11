using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DailyPlanner.Dto.Chat;
using DailyPlanner.Dto.Chatroom;
using DailyPlanner.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DailyPlanner.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IChatService _chatService;
        private ILogger<ChatController> _logger;

        public ChatController(
            IChatService chatService,
            ILogger<ChatController> logger)
        {
            _chatService = chatService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IEnumerable<ChatroomDto>> Get()
        {
            var chatroomDtos = await _chatService.GetAsync(HttpContext.User.GetId().Value);

            return chatroomDtos;
        }

        [HttpPost]
        public async Task<IEnumerable<ChatroomDto>> Upsert(IEnumerable<ChatroomDto> chatroomDtos)
        {
            var resultingChatroomDtos = await _chatService.UpsertAsync(chatroomDtos);

            return resultingChatroomDtos;
        }
    }
}