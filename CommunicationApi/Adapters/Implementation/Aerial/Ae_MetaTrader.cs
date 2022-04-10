using CommunicationApi.Adapters.Abstract;
using TestPackages.Bookkeepings;
using TestPackages.Utils.Enums;
using static TestPackages.Bookkeepings.AbstractOrder;

namespace CommunicationApi.Adapters.Implementation.Aerial
{
    public class Ae_MetaTrader : AbstractAerial
    {
        // Konstruktor
        public Ae_MetaTrader():base() { }

        protected override void OnStartHandler()
        {
            throw new NotImplementedException();
        }

        protected override void OnStopHandler()
        {
            throw new NotImplementedException();
        }

        public override AbstractOrder CloseOrder(AbstractOrder order, object token)
        {
            throw new NotImplementedException();
        }

        public override ExpertAdvisorStateType EAStatusRequest()
        {
            throw new NotImplementedException();
        }

        public override AbstractOrder OpenOrder(DirectionType direction, double volume, double openPrice, object token)
        {
            throw new NotImplementedException();
        }

        public override AbstractOrder OrderStatus(AbstractOrder order, object token)
        {
            throw new NotImplementedException();
        }
    }
}
