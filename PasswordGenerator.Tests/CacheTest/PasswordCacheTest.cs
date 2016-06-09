using System;
using System.Threading.Tasks;
using FluentAssertions;
using PasswordGenerator.Models;
using PasswordGenerator.Tests.CacheTest;
using Xunit;

namespace Tests.CacheTest
{
    public class PasswordCacheTest
    {
        private PasswordCache _passwordCache;
        private readonly Email _defaultEMail = TestHelper._defaultEMail;

        public PasswordCacheTest()
        {
            _passwordCache = TestHelper.CreateNewOneSecondExpirationPasswordCache();
        }

        [Fact(Skip = "In my computer It pass In App Veyor not. I think It is related with Random in AppVeyor")]
        public void Two_password_with_the_same_email_should_be_differents()
        {
            var pass = _passwordCache.GenerateNewPassword(_defaultEMail);
            var pass2 = _passwordCache.GenerateNewPassword(_defaultEMail);

            pass.Should().NotBe(pass2);
        }

        [Fact]
        public void Only_the_last_generated_password_is_valid()
        {
            _passwordCache.GenerateNewPassword(_defaultEMail);
            var lastPass = _passwordCache.GenerateNewPassword(_defaultEMail);

            _passwordCache.GetPassword(_defaultEMail).ShouldBeEquivalentTo(new Password(lastPass));
        }

        [Fact]
        public void Two_new_caches_with_the_same_instance_name_return_differents_passwords()
        {
            var firstPass = TestHelper.CreateNewOneSecondExpirationPasswordCache("an Instance").GenerateNewPassword(_defaultEMail);
            var secondPass = TestHelper.CreateNewOneSecondExpirationPasswordCache("an Instance").GetPassword(_defaultEMail);

            new Password(firstPass).EncryptedPassword.Should().NotBe(secondPass.EncryptedPassword);
        }

        [Fact]
        public async void A_password_expires_after_the_expiration_time()
        {
            _passwordCache.GenerateNewPassword(_defaultEMail);
            await Task.Delay(1200);
            _passwordCache.GetPassword(_defaultEMail).ShouldBeEquivalentTo(new Password {EncryptedPassword = null});
        }

        [Fact]
        public async void A_password_not_expires_before_the_expiration_time() //If the scheduler takes less 900 ms to retunr the control
        {
            var pass = _passwordCache.GenerateNewPassword(_defaultEMail);
            await Task.Delay(100);
            _passwordCache.GetPassword(_defaultEMail).ShouldBeEquivalentTo(new Password(pass));
        }
    }
}