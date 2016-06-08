using System;
using PasswordGenerator.Models;

namespace PasswordGenerator.Tests.CacheTest
{
    public class TestHelper
    {
        public static PasswordCache CreateNewOneSecondExpirationPasswordCache(string instanceName = null)
        {
            return new PasswordCache(instanceName ?? new Random().Next(10000000).ToString(), 1);
        }

        public static Email _defaultEMail = new Email { Value = "email@email.com" };

    }
}