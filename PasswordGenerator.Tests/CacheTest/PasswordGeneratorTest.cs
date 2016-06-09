using System;
using System.Threading.Tasks;
using FluentAssertions;
using PasswordGenerator.Models;
using PasswordGenerator.Tests.CacheTest;
using Xunit;

namespace Tests.CacheTest
{
    public class PasswordGeneratorTest
    {
        private PasswordGenerator.Models.PasswordGenerator Generator { get; set; }
        private readonly Email _defaultEMail = TestHelper._defaultEMail;

        public PasswordGeneratorTest()
        {
            var cache = TestHelper.CreateNewOneSecondExpirationPasswordCache();
            this.Generator = new PasswordGenerator.Models.PasswordGenerator(cache);
        }

        [Fact]
        public void Generate_password_with_empty_email_throw_exception()
        {
            Action act = (() => Generator.GenerateNew(new Email()));
            act.ShouldThrow<EmptyMailException>();
        }

        [Fact]
        public async void Get_an_expired_password_throw_an_exception()
        {
            var pass = Generator.GenerateNew(_defaultEMail);
            await Task.Delay(1100);
            Action act = () => Generator.GetOne(_defaultEMail);
            act.ShouldThrow<ExpiredOrUnextistenEncriptedPasswordException>();
        }

    }
}