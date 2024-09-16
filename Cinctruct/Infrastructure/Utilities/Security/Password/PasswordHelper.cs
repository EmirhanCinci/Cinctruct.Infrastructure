using System.Security.Cryptography;
using System.Text;

namespace Infrastructure.Utilities.Security.Password
{
	/// <summary>
	/// Provides utility methods for creating and verifying password hashes using HMACSHA512.
	/// </summary>
	public static class PasswordHelper
	{
		/// <summary>
		/// Creates a password hash and salt using HMACSHA512.
		/// </summary>
		/// <param name="password">The plain-text password to hash.</param>
		/// <param name="passwordHash">The generated password hash.</param>
		/// <param name="passwordSalt">The generated salt used for the hash.</param>
		public static void CreatePasswordHashByHmacSha512(string password, out byte[] passwordHash, out byte[] passwordSalt)
		{
			using (var hmac = new HMACSHA512())
			{
				passwordSalt = hmac.Key;
				passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
			}
		}

		/// <summary>
		/// Verifies a password against a stored hash using HMACSHA512.
		/// </summary>
		/// <param name="password">The plain-text password to verify.</param>
		/// <param name="passwordHash">The stored hash to compare against.</param>
		/// <param name="passwordSalt">The salt used to compute the stored hash.</param>
		/// <returns>True if the password matches the stored hash; otherwise, false.</returns>
		public static bool VerifyPasswordHashByHmacSha512(string password, byte[] passwordHash, byte[] passwordSalt)
		{
			using (var hmac = new HMACSHA512(passwordSalt))
			{
				var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
				for (int i = 0; i < computedHash.Length; i++)
				{
					if (computedHash[i] != passwordHash[i])
					{
						return false;
					}
				}
			}
			return true;
		}

		/// <summary>
		/// Creates a password hash and salt and returns them as a tuple.
		/// </summary>
		/// <param name="password">The plain-text password to hash.</param>
		/// <returns>A tuple containing the generated password hash and salt.</returns>
		public static (byte[], byte[]) CreatePasswordByHmacSha512(string password)
		{
			byte[] passwordHash, passwordSalt;
			CreatePasswordHashByHmacSha512(password, out passwordHash, out passwordSalt);
			return (passwordHash, passwordSalt);
		}
	}
}
