using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Security.Cryptography;
using System.Text;

namespace DailyPlanner.Infrastructure
{
    public class PasswordHashManager : IPasswordHashManager
    {
        public (string hash, byte[] salt) GenerateHashSaltPair(string password)
        {
            var saltBytes = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(saltBytes);
            }

            var hashed = GetHash(password, saltBytes);

            return (hash: hashed, salt: saltBytes);
        }

        public bool IsPasswordValid(string password, string hash, byte[] salt)
        {
            var hashedPassword = GetHash(password, salt);

            return hashedPassword == hash;
        }

        private string GetHash(string password, byte[] saltBytes)
            => Convert.ToBase64String(
                KeyDerivation.Pbkdf2(
                    password: password,
                    salt: saltBytes,
                    prf: KeyDerivationPrf.HMACSHA256,
                    iterationCount: 10000,
                    numBytesRequested: 256 / 8));
    }
}
