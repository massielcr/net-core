using System.Text.RegularExpressions;

namespace PSDDDCQRSInPracticeLB.Api.Entities.ValueObjects
{
    public class EmailAddress : ValueObject
    {
        private static readonly Regex EmailRegex = new (@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        public string Value { get; private set; }

        private EmailAddress() { }

        private EmailAddress(string value)
        {
            Value = value;
        }

        public static EmailAddress Create(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException("Email address cannot be null or empty.", nameof(email));
            }

            var normalizedEmail = email.Trim().ToLowerInvariant();

            if (!IsValidEmail(normalizedEmail))
            {
                throw new ArgumentException($"Invalid email address format: {email}", nameof(email));
            }

            if (normalizedEmail.Length > 254)
            {
                throw new ArgumentException("Email address is too long.", nameof(email));
            }

            return new EmailAddress(normalizedEmail);
        }

        public static bool IsValidEmail(string email)
        {
            if (!EmailRegex.IsMatch(email))
                return false;

            return true;                
        }

        public override string ToString()
        {
            return Value;
        }

        public static bool operator ==(EmailAddress left, EmailAddress right)
        {
            return EqualOperator(left, right);
        }

        public static bool operator !=(EmailAddress left, EmailAddress right)
        {
            return NotEqualOperator(left, right);
        }

        public static implicit operator string(EmailAddress emailAddress)
        {
            return emailAddress?.Value;
        }

        public static explicit operator EmailAddress(string email)
        {
            return Create(email);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value?.ToLowerInvariant();
        }
    }
}
