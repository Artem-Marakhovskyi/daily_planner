using System;
using System.Linq;
using System.Security.Claims;

namespace DailyPlanner.Api
{
    public static class ClaimsPrincipalExtensions
    {
        public const string IdType = "id";
        public const string EmailType = "email";

        public static Guid? GetId(this ClaimsPrincipal claimsPrincipal)
        {
            var value = claimsPrincipal?.Claims?.FirstOrDefault(e => e.Type == IdType).Value;

            if (string.IsNullOrEmpty(value))
            {
                return null;
            }

            return Guid.Parse(value);
        }

        public static string GetEmail(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal?.Claims?.FirstOrDefault(e => e.Type == EmailType).Value;
        }
    }
}
