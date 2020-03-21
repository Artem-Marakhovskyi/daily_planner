using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DailyPlanner.Dto;
using DailyPlanner.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DailyPlanner.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class PersonController : ControllerBase
    {
        private readonly IPeopleService _peopleService;
        private readonly ILogger<PersonController> _logger;

        public PersonController(
            IPeopleService peopleService,
            ILogger<PersonController> logger)
        {
            _peopleService = peopleService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IEnumerable<PersonDto>> Get()
        {
            var people = await _peopleService.GetAsync();

            return people;
        }

        [HttpGet]
        [Route("/person/{email}")]

        public async Task<PersonDto> Get(string email)
        {
            var person = await _peopleService.GetAsync(email);

            if (person == null)
            {
                return null;
            }

            return person;
        }
    }
}
