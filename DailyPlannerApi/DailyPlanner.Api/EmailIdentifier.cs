using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DailyPlanner.Api
{
    public class EmailIdentifier
    {
        private readonly RequestDelegate _next;

        public EmailIdentifier(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            var claims = new List<Claim>();
            var idValue = httpContext.User?.Claims?.FirstOrDefault(e => e.Type == ClaimsPrincipalExtensions.IdType)?.Value;
            if (idValue != null)
            {
                claims.Add(new Claim("id", idValue));
            }

            var emailValue = httpContext.User?.Claims?.FirstOrDefault(e => e.Type.Contains(ClaimsPrincipalExtensions.EmailType))?.Value;
            if (emailValue != null)
            {
                claims.Add(new Claim("email", emailValue));
            }

            if (claims.Any())
            {
                httpContext.User = new ClaimsPrincipal(new ClaimsIdentity(claims, "BEARER"));
            }

            await _next(httpContext);
        }
    }
}
