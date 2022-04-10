using TestPackages.Bookkeepings;

namespace TestPackages.Utils.Bookkeepings
{
    public class TestOrder : AbstractOrder
    {
        public TestOrder(string id, string market, ORDER_STATUS status, DirectionType direction, DateTime openDate, double volume, double openPrice) : base(id, market, status, direction, openDate, volume, openPrice)
        {
        }
    }
}
