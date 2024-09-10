using System.Security.Cryptography;
using System.Text;

namespace Infrastructure.Utilities.Security.Password
{
    public static class PasswordHelper
    {
        public static void CreatePasswordHashByHmacSha512(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

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

        public static (byte[], byte[]) CreatePasswordByHmacSha512(string password)
        {
            byte[] passwordHash, passwordSalt;
            CreatePasswordHashByHmacSha512(password, out passwordHash, out passwordSalt);
            return (passwordHash, passwordSalt);
        }
    }
}
