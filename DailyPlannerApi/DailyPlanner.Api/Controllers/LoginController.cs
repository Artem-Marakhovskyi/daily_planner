using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DailyPlanner.Api.ViewDtos;
using DailyPlanner.Dto;
using DailyPlanner.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace DailyPlanner.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IPersonService _personService;
        private readonly IMapper _mapper;

        public LoginController(
            IPersonService personService,
            IMapper mapper)
        {
            _personService = personService;
            _mapper = mapper;
        }

        [HttpPost("/register")]
        public async Task<IActionResult> Register(PersonRegisterDto personRegisterDto)
        {
            var person = await _personService.CreateAsync(_mapper.Map<PersonDto>(personRegisterDto), personRegisterDto.Password);

            return Ok(GenerateSuccessfulTokenResponse(person));
        }


        [HttpPost("/token")]
        public async Task<IActionResult> Token(string email, string password)
        {
            if (string.IsNullOrWhiteSpace(email)) return BadRequest(new ParameterErrorDto(nameof(email)));
            if (string.IsNullOrWhiteSpace(password)) return BadRequest(new ParameterErrorDto(nameof(password)));

            var person = await _personService.GetAsync(email, password);
            
            if (person == null)
            {
                return BadRequest(new { errorText = "Invalid username or password." });
            }

            return Ok(GenerateSuccessfulTokenResponse(person));
        }

        private LoginResponseDto GenerateSuccessfulTokenResponse(PersonDto person)
        {
            var now = DateTime.UtcNow;
            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.Issuer,
                    audience: AuthOptions.Audience,
                    notBefore: now,
                    claims: new List<Claim>()
                    {
                        new Claim("email", person.Email),
                        new Claim("id", person.Id.ToString())
                    },
                    expires: now.Add(TimeSpan.FromHours(AuthOptions.LifetimeHours)),
                    signingCredentials: new SigningCredentials(
                        new SymmetricSecurityKey(Encoding.ASCII.GetBytes(AuthOptions.SecretKey)),
                        SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return new LoginResponseDto(encodedJwt, person.Email, person.FirstName, person.LastName);
        }
    }
}