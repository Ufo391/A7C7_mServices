using CommunicationApi.Exceptions;
using TestPackages.Bookkeepings;
using TestPackages.Utils.Charts;
using static TestPackages.Bookkeepings.AbstractOrder;

namespace CommunicationApi.Adapters.Abstract
{
    public abstract class AbstractReliability
    {
        // Attribute              
        protected Action<AbstractTick> OnTick = delegate { };        
        private AbstractSecurity security; 
        public AbstractSecurity Security
        {
            get
            {
                return security;                
            }
            internal set
            {
                if(security == null)
                {
                    security = value;
                    security.AddEventHandlerOnTick(OnTickHandler);
                }
                else
                {
                    throw new AbstractLayerException(AbstractLayerException.ERROR_CODES.LAYER_WAS_ALREADY_SET);
                }
            }
        }

        // Abstrakt
        abstract protected bool ReliabilityStrategy(); // StrategyPattern
        abstract protected AbstractTick StringTickToDerivateConversion(string tick);
        
        // Hilfsmethoden
        private void OnTickHandler(string tick)
        {
            OnTick(StringTickToDerivateConversion(tick));
        }

        // Methoden
        internal void AddEventHandlerOnTick(Action<AbstractTick> meth)
        {
            OnTick += meth;
        }

        internal AbstractOrder OpenOrder(DirectionType direction, double volume, double openPrice)
        {
            if (ReliabilityStrategy() == true)
            {
                var order = Security.OpenOrder(direction, volume, openPrice);

                return order;
            }
            else
            {
                throw new ReliabilityException(ReliabilityException.ERROR_CODE.EXECUTING_FAILED);
            }
        }

        internal AbstractOrder CloseOrder(AbstractOrder order)
        {
            if (ReliabilityStrategy() == true)
            {
                return Security.CloseOrder(order);
            }
            else
            {
                throw new ReliabilityException(ReliabilityException.ERROR_CODE.EXECUTING_FAILED);
            }
        }

        internal AbstractOrder OrderStatus(AbstractOrder order)
        {
            if (ReliabilityStrategy() == true)
            {
                return Security.OrderStatus(order);
            }
            else
            {
                throw new ReliabilityException(ReliabilityException.ERROR_CODE.EXECUTING_FAILED);
            }
        }

        public override bool Equals(object? obj)
        {
            return obj is AbstractReliability reliability &&
                   EqualityComparer<Action<AbstractTick>>.Default.Equals(OnTick, reliability.OnTick) &&
                   EqualityComparer<AbstractSecurity>.Default.Equals(security, reliability.security);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(OnTick, security);
        }
    }
}
