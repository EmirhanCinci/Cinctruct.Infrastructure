using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Infrastructure.Utilities.Security.Encyption
{
	/// <summary>
	/// Provides methods for creating security keys used for JWT token signing.
	/// </summary>
	public class SecurityKeyHelper
	{
		/// <summary>
		/// Creates a security key from the specified string.
		/// </summary>
		/// <param name="securityKey">The string representation of the security key.</param>
		/// <returns>A SecurityKey instance created from the provided string.</returns>
		public static SecurityKey CreateSecurityKey(string securityKey)
		{
			return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
		}
	}
}
