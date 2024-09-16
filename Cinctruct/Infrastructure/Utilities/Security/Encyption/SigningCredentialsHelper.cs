using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Utilities.Security.Encyption
{
	/// <summary>
	/// Provides methods for creating signing credentials for JWT tokens.
	/// </summary>
	public class SigningCredentialsHelper
	{
		/// <summary>
		/// Creates signing credentials using the specified security key.
		/// </summary>
		/// <param name="securityKey">The security key to use for signing credentials.</param>
		/// <returns>SigningCredentials instance initialized with the specified security key and HMAC SHA-256 algorithm.</returns>
		public static SigningCredentials CreateSigningCredentials(SecurityKey securityKey)
		{
			return new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
		}
	}
}
