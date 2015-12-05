using System;
using System.Security.Cryptography;

namespace BabyDiary.Business.Helpers
{
    public class PasswordHash
    {
        public const int SaltByteSize = 64;
        public const int HashByteSize = 64;
        public const int Pbkdf2Iterations = 5000;

        public static string CreateRandomHash()
        {
            byte[] salt = CreateSalt();
            return Convert.ToBase64String(salt);
        }

        private static byte[] CreateSalt()
        {
            RNGCryptoServiceProvider csprng = new RNGCryptoServiceProvider();
            byte[] salt = new byte[SaltByteSize];
            csprng.GetBytes(salt);
            return salt;
        }

        /// <summary>
        /// Creates a salted PBKDF2 hash of the password.
        /// </summary>
        /// <param name="password">The password to hash.</param>
        /// <returns>The hash of the password.</returns>
        public static string CreateHash(string password)
        {
            // Generate a random salt
            byte[] salt = CreateSalt();

            // Hash the password and encode the parameters
            byte[] hash = PBKDF2(password, salt, Pbkdf2Iterations, HashByteSize);

            var iterationsBytes = BitConverter.GetBytes(Pbkdf2Iterations);
            var valueToSave = new byte[SaltByteSize + HashByteSize + iterationsBytes.Length];
            Buffer.BlockCopy(salt, 0, valueToSave, 0, SaltByteSize);
            Buffer.BlockCopy(hash, 0, valueToSave, SaltByteSize, HashByteSize);
            Buffer.BlockCopy(iterationsBytes, 0, valueToSave, SaltByteSize + HashByteSize, iterationsBytes.Length);

            return Convert.ToBase64String(valueToSave);
        }

        /// <summary>
        /// Validates a password given a hash of the correct one.
        /// </summary>
        /// <param name="password">The password to check.</param>
        /// <param name="correctHash">A hash of the correct password.</param>
        /// <returns>True if the password is correct. False otherwise.</returns>
        public static bool ValidatePassword(string password, string correctHash)
        {
            var salt = new byte[SaltByteSize];
            var savedPasswordBytes = new byte[HashByteSize];

            var savedHashBytes = Convert.FromBase64String(correctHash);

            var iterationsLength = savedHashBytes.Length - (SaltByteSize + HashByteSize);
            var iterationsBytes = new byte[iterationsLength];
            Buffer.BlockCopy(savedHashBytes, 0, salt, 0, SaltByteSize);
            Buffer.BlockCopy(savedHashBytes, SaltByteSize, savedPasswordBytes, 0, savedPasswordBytes.Length);
            Buffer.BlockCopy(savedHashBytes, (salt.Length + savedPasswordBytes.Length), iterationsBytes, 0, iterationsLength);

            byte[] testHash = PBKDF2(password, salt, BitConverter.ToInt32(iterationsBytes, 0), HashByteSize);
            return SlowEquals(savedHashBytes, testHash);
        }

        /// <summary>
        /// Compares two byte arrays in length-constant time. This comparison
        /// method is used so that password hashes cannot be extracted from
        /// on-line systems using a timing attack and then attacked off-line.
        /// </summary>
        /// <param name="a">The first byte array.</param>
        /// <param name="b">The second byte array.</param>
        /// <returns>True if both byte arrays are equal. False otherwise.</returns>
        private static bool SlowEquals(byte[] a, byte[] b)
        {
            uint diff = (uint)a.Length ^ (uint)b.Length;
            for (int i = 0; i < a.Length && i < b.Length; i++)
                diff |= (uint)(a[i] ^ b[i]);
            return diff == 0;
        }

        /// <summary>
        /// Computes the PBKDF2-SHA1 hash of a password.
        /// </summary>
        /// <param name="password">The password to hash.</param>
        /// <param name="salt">The salt.</param>
        /// <param name="iterations">The PBKDF2 iteration count.</param>
        /// <param name="outputBytes">The length of the hash to generate, in bytes.</param>
        /// <returns>A hash of the password.</returns>
        private static byte[] PBKDF2(string password, byte[] salt, int iterations, int outputBytes)
        {
            byte[] hashValue;
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations))
            {
                hashValue = pbkdf2.GetBytes(outputBytes);
            }
            return hashValue;
        }
    }
}