using Infrastructure.Constants;
using System.Security.Cryptography;

namespace Infrastructure.Utilities.Security.Password
{
    public class PasswordGenerator
    {
        private static readonly char[] Characters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!@#$%^&*()".ToCharArray();

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
