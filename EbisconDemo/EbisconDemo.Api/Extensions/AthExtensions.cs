using EbisconDemo.Data.Models;
using System.Security.Claims;

namespace EbisconDemo.Api.Extensions
{
    public static class AthExtensions
    {
        private const string JwtSub = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier";

        public static int GetCurrentUserId(this ClaimsPrincipal user)
        {
            return int.Parse(user.FindFirst(JwtSub)!.Value);
        }
    }
}
