using CommunicationApi.Adapters.Abstract;
using System;
using TestPackages.Bookkeepings;
using TestPackages.Utils.Bookkeepings;
using TestPackages.Utils.Enums;

namespace CommunicationApiTest.Adapters.Implementation
{
    public class TestAerial : AbstractAerial
    {
        public InternalStateType InternalState { get; set; }
        public bool IsOrderClosedRequested = false;
        public bool IsOrderOpenedRequested = false;
        public bool IsEAStatusRequested = false;
        public bool IsOrderStatusRequested = false;

        public enum InternalStateType
        {
            Init, Start, Stop
        }

        protected override void OnStartHandler()
        {
            InternalState = InternalStateType.Start;
        }

        protected override void OnStopHandler()
        {
            InternalState = InternalStateType.Stop;
        }

        public override AbstractOrder CloseOrder(AbstractOrder order, object token)
        {
            IsOrderClosedRequested = true;
            order.Status = AbstractOrder.ORDER_STATUS.CLOSED;
            return order;
        }

        public override ExpertAdvisorStateType EAStatusRequest()
        {
            IsEAStatusRequested = true;
            throw new System.NotImplementedException();
        }

        public override AbstractOrder OpenOrder(AbstractOrder.DirectionType direction, double volume, double openPrice, object token)
        {
            IsOrderOpenedRequested = true;
            return new ForexOrder("id", "market", AbstractOrder.ORDER_STATUS.OPEN, direction, DateTime.Now, volume, openPrice, "tradingPair");
        }

        public override AbstractOrder OrderStatus(AbstractOrder order, object token)
        {
            IsOrderStatusRequested = true;
            return order;
        }
    }
}
