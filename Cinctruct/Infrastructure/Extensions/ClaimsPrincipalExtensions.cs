using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Infrastructure.Extensions
{
	/// <summary>
	/// Provides extension methods for retrieving claims from a <see cref="ClaimsPrincipal"/> object.
	/// </summary>
	public static class ClaimsPrincipalExtensions
	{
		/// <summary>
		/// Retrieves the email claim value from the <see cref="ClaimsPrincipal"/> object.
		/// </summary>
		/// <param name="claimsPrincipal">The <see cref="ClaimsPrincipal"/> object from which to retrieve the email claim.</param>
		/// <returns>The email value if present; otherwise, <c>null</c>. </returns>
		public static string? GetEmail(this ClaimsPrincipal claimsPrincipal)
		{
			return claimsPrincipal.FindFirstValue(JwtRegisteredClaimNames.Email);
		}

		/// <summary>
		/// Retrieves the name claim value from the <see cref="ClaimsPrincipal"/> object.
		/// </summary>
		/// <param name="claimsPrincipal">The <see cref="ClaimsPrincipal"/> object from which to retrieve the name claim.</param>
		/// <returns>The name value if present; otherwise, <c>null</c>.</returns>
		public static string? GetName(this ClaimsPrincipal claimsPrincipal)
		{
			return claimsPrincipal.FindFirstValue(ClaimTypes.Name);
		}

		/// <summary>
		/// Retrieves the name identifier claim value from the <see cref="ClaimsPrincipal"/> object.
		/// </summary>
		/// <param name="claimsPrincipal">The <see cref="ClaimsPrincipal"/> object from which to retrieve the name identifier claim.</param>
		/// <returns>The name identifier value if present; otherwise, <c>null</c>.</returns>
		public static string? GetNameIdentifier(this ClaimsPrincipal claimsPrincipal)
		{
			return claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
		}

		/// <summary>
		/// Retrieves all role claims from the <see cref="ClaimsPrincipal"/> object.
		/// </summary>
		/// <param name="claimsPrincipal">The <see cref="ClaimsPrincipal"/> object from which to retrieve the role claims.</param>
		/// <returns>An array of role values. If no roles are found, returns an empty array.</returns>
		public static string[] GetRoles(this ClaimsPrincipal claimsPrincipal)
		{
			var roles = claimsPrincipal.FindAll(ClaimTypes.Role).Select(claim => claim.Value).ToArray();
			return roles.Length > 0 ? roles : Array.Empty<string>();
		}
	}
}
