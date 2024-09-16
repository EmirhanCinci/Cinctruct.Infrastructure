using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Infrastructure.Extensions
{
	/// <summary>
	/// Provides extension methods for adding claims to a collection of claims.
	/// </summary>
	public static class ClaimExtensions
	{
		/// <summary>
		/// Adds an email claim to the collection of claims.
		/// </summary>
		/// <param name="claims">The collection of claims to which the email claim will be added.</param>
		/// <param name="email">The email address to be added as a claim.</param>
		public static void AddEmail(this ICollection<Claim> claims, string email)
		{
			claims.Add(new Claim(JwtRegisteredClaimNames.Email, email));
		}

		/// <summary>
		/// Adds a name claim to the collection of claims.
		/// </summary>
		/// <param name="claims">The collection of claims to which the name claim will be added.</param>
		/// <param name="name">The name to be added as a claim.</param>
		public static void AddName(this ICollection<Claim> claims, string name)
		{
			claims.Add(new Claim(ClaimTypes.Name, name));
		}

		/// <summary>
		/// Adds a name identifier claim to the collection of claims.
		/// </summary>
		/// <param name="claims">The collection of claims to which the name identifier claim will be added.</param>
		/// <param name="nameIdentifier">The unique identifier to be added as a claim.</param>
		public static void AddNameIdentifier(this ICollection<Claim> claims, string nameIdentifier)
		{
			claims.Add(new Claim(ClaimTypes.NameIdentifier, nameIdentifier));
		}

		/// <summary>
		/// Adds multiple role claims to the collection of claims.
		/// </summary>
		/// <param name="claims">The collection of claims to which the role claims will be added.</param>
		/// <param name="roles">The array of roles to be added as claims.</param>
		public static void AddRoles(this ICollection<Claim> claims, string[] roles)
		{
			roles.ToList().ForEach(role => claims.Add(new Claim(ClaimTypes.Role, role)));
		}
	}
}
