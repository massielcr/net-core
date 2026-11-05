using Microsoft.Identity.Client.NativeInterop;

namespace PSDDDCQRSInPracticeLB.Api.Entities.ValueObjects
{
    public class Money : ValueObject
    {
        public decimal Amount { get; set; }
        public string Currency { get; set; }

        private Money() { }

        private Money(decimal amount, string currency)
        {
            if (string.IsNullOrWhiteSpace(currency))
            {
                throw new ArgumentException("Currency must be provided.", nameof(currency));
            }

            Amount = amount;
            Currency = currency;
        }

        public static Money Create(decimal amount, string currency) => new (amount, currency);

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Amount;
            yield return Currency;
        }
    }
}
