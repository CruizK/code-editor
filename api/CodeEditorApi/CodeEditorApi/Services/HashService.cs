using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Security.Cryptography;

namespace CodeEditorApi.Services
{
    public interface IHashService
    {
        string HashPassword(string password, byte[] salt = null);
        string HashPassword(string password, string salt);
        bool ComparePassword(string hash, string password);
    }

    public class HashService : IHashService
    {
        private const int iterationCount = 10000;

        public string HashPassword(string password, byte[] salt = null)
        {
            if(salt == null)
            {
                salt = new byte[128 / 8];
                using (var rngCsp = new RNGCryptoServiceProvider())
                {
                    rngCsp.GetNonZeroBytes(salt);
                }
            }
            

            string hash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: iterationCount,
                numBytesRequested: 256 / 8));

            // Algo-Itr-Salt-Hash
            string finalHash = string.Format("{0}-{1}-{2}-{3}", "Pbkdf2", iterationCount, Convert.ToBase64String(salt), hash);
            return finalHash;
        }

        public string HashPassword(string password, string salt)
        {
            return HashPassword(password, Convert.FromBase64String(salt));
        }

        public bool ComparePassword(string hash, string password)
        {
            var parts = hash.Split('-');
            var salt = parts[2];

            return HashPassword(password, salt) == hash;
        }
    }
}
