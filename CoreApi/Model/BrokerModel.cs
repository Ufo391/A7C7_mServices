using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreApi.Model
{
    public class BrokerAccount
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }
        public string RegisterId { get; set; }
        public double Balance { get; set; }

        [ForeignKey(nameof(Currency))]
        public Guid CurrencyId { get; set; }
        public ValueModel Currency { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is BrokerAccount account &&
                   Name == account.Name &&
                   RegisterId == account.RegisterId &&
                   Balance == account.Balance &&
                   EqualityComparer<ValueModel>.Default.Equals(Currency, account.Currency);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, RegisterId, Balance, Currency);
        }
    }
}
