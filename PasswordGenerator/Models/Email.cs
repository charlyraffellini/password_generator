namespace PasswordGenerator.Models
{
    public class Email
    {
        public Email()
        {
        }

        public Email(string value)
        {
            Value = value;
        }

        public string Value { get; set; }
    }
}