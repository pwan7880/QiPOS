using System;
using System.Security.Cryptography;

namespace QiPOS
{
    public static class PasswordHasher
    {
        private const int SaltSize = 16; // 128 bit
        private const int KeySize = 32; // 256 bit
        private const int Iterations = 10000;

        public static string HashPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Password cannot be empty", nameof(password));

            byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);
            byte[] key = DeriveKey(password, salt, Iterations);

            return string.Format("{0}.{1}.{2}", Iterations, Convert.ToBase64String(salt), Convert.ToBase64String(key));
        }

        public static bool VerifyPassword(string password, string hashedPassword)
        {
            if (string.IsNullOrWhiteSpace(hashedPassword))
                return false;

            string[] parts = hashedPassword.Split('.');
            if (parts.Length != 3)
                return false;

            if (!int.TryParse(parts[0], out int iterations))
                return false;

            byte[] salt = Convert.FromBase64String(parts[1]);
            byte[] expectedKey = Convert.FromBase64String(parts[2]);
            byte[] actualKey = DeriveKey(password, salt, iterations);

            return CryptographicOperations.FixedTimeEquals(actualKey, expectedKey);
        }

        private static byte[] DeriveKey(string password, byte[] salt, int iterations)
        {
            using (var deriveBytes = new Rfc2898DeriveBytes(password, salt, iterations, HashAlgorithmName.SHA256))
            {
                return deriveBytes.GetBytes(KeySize);
            }
        }
    }
}
