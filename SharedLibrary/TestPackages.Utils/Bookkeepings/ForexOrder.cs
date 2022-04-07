using TestPackages.Bookkeepings;

namespace TestPackages.Utils.Bookkeepings
{
    public class ForexOrder : AbstractOrder
    {
        public string TradingPair { get; init; }

        public ForexOrder(string id, string market, ORDER_STATUS status, DirectionType direction, DateTime openDate, double volume, double openPrice, string tradingPair) : base(id, market, status, direction, openDate, volume, openPrice)
        {
            TradingPair = tradingPair;
        }

        public override string ToString()
        {
            return base.ToString() + $" | Pair: {TradingPair}";
        }

        public override bool Equals(object? obj)
        {
            return obj is ForexOrder order &&
                   base.Equals(obj) &&
                   TradingPair == order.TradingPair;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode(), TradingPair);
        }
    }
}
