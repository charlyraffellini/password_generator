using System;
using System.Web.WebPages;

namespace PasswordGenerator.Models
{
    public class PasswordGenerator
    {
        private PasswordCache Cache { get; set; }

        public PasswordGenerator(PasswordCache cache)
        {
            this.Cache = cache;
        }

        public string GenerateNew(Email email)
        {
            this.ValidateEmail(email);
            return this.Cache.GenerateNewPassword(email);
        }

        public Password GetOne(Email email)
        {
            var password = this.Cache.GetPassword(email);
            return this.ValidatePassword(password);
        }

        private Password ValidatePassword(Password password)
        {
            if (password.EncryptedPassword.IsEmpty()) throw new ExpiredOrUnextistenEncriptedPasswordException();
            return password;
        }

        private void ValidateEmail(Email email)
        {
            if(email.Value.IsEmpty()) throw new EmptyMailException();
        }
    }

    public class ExpiredOrUnextistenEncriptedPasswordException : Exception
    {
    }

    public class EmptyMailException : Exception
    {
    }
}