using System;
using PasswordGenerator.Models;

namespace PasswordGenerator.Tests.CacheTest
{
    public class TestHelper
    {
        public static PasswordCache CreateNewOneSecondExpirationPasswordCache(string instanceName = null)
        {
            return new PasswordCache(instanceName ?? new Random().Next(10000000).ToString(), 1);//TODO this 1 is a magic number. Extract to constant and use in Task.Delay() in tests 
        }

        public static Email _defaultEMail = new Email("email@email.com");

    }
}
