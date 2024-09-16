using Infrastructure.Constants;
using System.Security.Cryptography;

namespace Infrastructure.Utilities.Security.Password
{
	/// <summary>
	/// Provides methods for generating random passwords.
	/// </summary>
	public class PasswordGenerator
	{
		private static readonly char[] Characters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!@#$%^&*()".ToCharArray();

		/// <summary>
		/// Generates a random password of the specified length.
		/// </summary>
		/// <param name="length">The length of the generated password. Defaults to 8 if not specified.</param>
		/// <returns>A randomly generated password.</returns>
		/// <exception cref="ArgumentException">Thrown when the specified length is less than 1.</exception>
		public static string GeneratePassword(int length = 8)
		{
			if (length < 1) throw new ArgumentException(SystemMessages.InvalidLengthPassword);
			using (var rng = new RNGCryptoServiceProvider())
			{
				var byteBuffer = new byte[length];
				rng.GetBytes(byteBuffer);
				var passwordChars = byteBuffer.Select(b => Characters[b % Characters.Length]).ToArray();
				return new string(passwordChars);
			}
		}
	}
}
