using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Caching;

namespace PasswordGenerator.Models
{
    public class PasswordCache
    {
        [SuppressMessage("ReSharper", "InconsistentNaming")]
        private const int THOUSAND = 1000;
        [SuppressMessage("ReSharper", "InconsistentNaming")]
        private const int EXPIRATION_IN_SECONDS = 30;

        private int ExpirationTimeInSeconds { get; set; }

        private MemoryCache Cache { get; set; }

        public PasswordCache()
        {
            this.ExpirationTimeInSeconds = EXPIRATION_IN_SECONDS;
            this.Cache = new MemoryCache("PasswordCache");
        }

        public PasswordCache(string instanceName, int expirationTimeInSeconds)
        {
            ExpirationTimeInSeconds = expirationTimeInSeconds;
            this.Cache = new MemoryCache(instanceName);
        }

        public string GenerateNewPassword(Email email)
        {
            var rawPassword = new Random().Next(THOUSAND * THOUSAND).ToString("D6");
            var password = new Password(rawPassword);
            var absoluteExpiration = DateTime.Now.Add(TimeSpan.FromSeconds(this.ExpirationTimeInSeconds));
            this.Cache.Set(email.Value, password.EncryptedPassword, new CacheItemPolicy {AbsoluteExpiration = absoluteExpiration});
            return rawPassword;
        }

        public Password GetPassword(Email email)
        {
            return new Password { EncryptedPassword = (string) this.Cache.Get(email.Value)};
        }
    }
}