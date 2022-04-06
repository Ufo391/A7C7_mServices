using CommunicationApi.Exceptions;
using TestPackages.Bookkeepings;
using static TestPackages.Bookkeepings.AbstractOrder;

namespace CommunicationApi.Adapters.Abstract
{
    public abstract class AbstractSecurity
    {
        // Attribute
        protected Action<string> OnTick = delegate { };
        protected AbstractAerial _aerial;
        public AbstractAerial ReceiveTransmit
        {
            get
            {
                return _aerial;
            }
            set
            {
                if (_aerial == null)
                {
                    _aerial = value;
                    _aerial.AddEventHandlerOnTick(OnTickHandler);
                }
                else
                {
                    throw new AbstractLayerException(AbstractLayerException.ERROR_CODES.LAYER_WAS_ALREADY_SET);
                }
            }
        }

        // Abstrakt
        abstract protected bool Validate(HttpContext httpContext);
        abstract protected string SecurityStrategy(); // StrategyPattern        

        // Hilfsmethoden
        protected void OnTickHandler(string tick, HttpContext httpContext)
        {
            if(Validate(httpContext) == true)
            {
                OnTick(tick);
            }
            else
            {
                throw new SecurityException(SecurityException.ERROR_CODE.SECURITY_VALIDATION_FAILED, "Unauthorisierter Zugriff");
            }
        }
        public void AddEventHandlerOnTick(Action<string> meth)
        {
            OnTick += meth;
        }
        public AbstractOrder OpenOrder(DirectionType direction, double volume, double openPrice)
        {
            var token = SecurityStrategy();
            return ReceiveTransmit.OpenOrder(direction, volume, openPrice, token);
        }
        public AbstractOrder CloseOrder(AbstractOrder order)
        {
            var token = SecurityStrategy();
            return ReceiveTransmit.CloseOrder(order, token);
        }
        public AbstractOrder OrderStatus(AbstractOrder order)
        {
            var token = SecurityStrategy();
            return ReceiveTransmit.OrderStatus(order, token);
        }
    }
}
