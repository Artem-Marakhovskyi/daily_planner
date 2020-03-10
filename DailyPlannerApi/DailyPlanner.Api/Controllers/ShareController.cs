using DailyPlanner.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace DailyPlanner.Api.Controllers
{
    [ApiController]
    [Authorize]    
    public class ShareController : ControllerBase
    {
        private readonly INotesService _notesService;
        private readonly IEventsService _eventsService;
        private readonly IRemindersService _remindersService;
        private readonly ILogger<ShareController> _logger;

        public ShareController(
            INotesService notesService,
            IEventsService eventsService,
            IRemindersService remindersService,
            ILogger<ShareController> logger)
        {
            _notesService = notesService;
            _eventsService = eventsService;
            _remindersService = remindersService;
            _logger = logger;
        }


    }
}
