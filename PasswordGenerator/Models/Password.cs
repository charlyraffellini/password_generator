using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace PasswordGenerator.Models
{
    public class Password
    {
        private static readonly SHA1CryptoServiceProvider CriptoProvider = new SHA1CryptoServiceProvider();
        public string EncryptedPassword { get; set; }

        public Password(string rawValue)
        {
            var value = new MemoryStream(GetBytes(rawValue));
            var sha1data = CriptoProvider.ComputeHash(value);
            this.EncryptedPassword = Encoding.ASCII.GetString(sha1data);
        }

        public Password() {}


        static byte[] GetBytes(string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }
    }
}