using CommunicationApi.Exceptions;
using TestPackages.Bookkeepings;
using TestPackages.Utils.Charts;
using TestPackages.Utils.Enums;
using static TestPackages.Bookkeepings.AbstractOrder;

namespace CommunicationApi.Adapters.Abstract
{
    public abstract class AbstractReliability
    {
        // Attributes              
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

        // Abstract
        abstract protected bool ReliabilityStrategy(); // StrategyPattern
        abstract protected AbstractTick StringTickToDerivateConversion(string tick);
        
        // Helping methods
        private void OnTickHandler(string tick)
        {
            OnTick(StringTickToDerivateConversion(tick));
        }

        // Methods
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

        public void ChangeExpertAdvisorStateType(ExpertAdvisorStateType state)
        {
            security.ChangeExpertAdvisorStateType(state);
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
