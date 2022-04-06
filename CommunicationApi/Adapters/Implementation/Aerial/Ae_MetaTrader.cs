using CommunicationApi.Adapters.Abstract;
using TestPackages.Bookkeepings;
using TestPackages.Utils.Enums;
using static TestPackages.Bookkeepings.AbstractOrder;

namespace CommunicationApi.Adapters.Implementation.Aerial
{
    public class Ae_MetaTrader : AbstractAerial
    {
        // Konstruktor
        public Ae_MetaTrader() { }

        internal override AbstractOrder CloseOrder(AbstractOrder order, object token)
        {
            throw new NotImplementedException();
        }

        internal override ExpertAdvisorStateType EAStatusRequest()
        {
            throw new NotImplementedException();
        }

        internal override AbstractOrder OpenOrder(DirectionType direction, double volume, double openPrice, object token)
        {
            throw new NotImplementedException();
        }

        internal override AbstractOrder OrderStatus(AbstractOrder order, object token)
        {
            throw new NotImplementedException();
        }
    }
}
