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
        protected AbstractSecurity _security; 
        public AbstractSecurity Security
        {
            get
            {
                return _security;                
            }
            set
            {
                if(_security == null)
                {
                    _security = value;
                    _security.AddEventHandlerOnTick(OnTickHandler);
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
        protected void OnTickHandler(string tick)
        {
            OnTick(StringTickToDerivateConversion(tick));
        }

        // Methoden
        public void AddEventHandlerOnTick(Action<AbstractTick> meth)
        {
            OnTick += meth;
        }

        public AbstractOrder OpenOrder(DirectionType direction, double volume, double openPrice)
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

        public AbstractOrder CloseOrder(AbstractOrder order)
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
        
        public AbstractOrder OrderStatus(AbstractOrder order)
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
    }
}
