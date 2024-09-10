using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Infrastructure.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static string? GetEmail(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.FindFirstValue(JwtRegisteredClaimNames.Email);
        }

        public static string? GetName(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.FindFirstValue(ClaimTypes.Name);
        }

        public static string? GetNameIdentifier(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        public static string[] GetRoles(this ClaimsPrincipal claimsPrincipal)
        {
            var roles = claimsPrincipal.FindAll(ClaimTypes.Role).Select(claim => claim.Value).ToArray();
            return roles.Length > 0 ? roles : Array.Empty<string>();
        }
    }
}
