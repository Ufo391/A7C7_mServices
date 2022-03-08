using Communication.Enums;
using ExpertAdvisors.Abstract;
using ExpertAdvisors.Model.Orders;
using static ExpertAdvisors.Model.Orders.AbstractOrder;

namespace ExpertAdvisors._04_ReceiveTransmit
{
    public class RT_MetaTrader : AbstractAerial
    {
        // Konstruktor
        public RT_MetaTrader() { }

        public override AbstractOrder CloseOrder(AbstractOrder order, string token)
        {
            throw new System.NotImplementedException();
        }

        public override STATUS_CODES EAStatusRequest(out object response)
        {
            throw new System.NotImplementedException();
        }

        public override AbstractOrder OpenOrder(DIRECTION direction, double volume, double openPrice, string token)
        {
            throw new System.NotImplementedException();
        }

        public override AbstractOrder OrderStatus(AbstractOrder order, string token)
        {
            throw new System.NotImplementedException();
        }
    }
}
