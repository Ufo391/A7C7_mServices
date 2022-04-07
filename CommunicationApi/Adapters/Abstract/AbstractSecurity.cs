using CommunicationApi.Exceptions;
using TestPackages.Bookkeepings;
using TestPackages.Utils.Enums;
using static TestPackages.Bookkeepings.AbstractOrder;

namespace CommunicationApi.Adapters.Abstract
{
    public abstract class AbstractSecurity
    {
        // Attributes
        protected Action<string> OnTick = delegate { };
        private AbstractAerial aerial;
        public AbstractAerial Aerial
        {
            get
            {
                return aerial;
            }
            internal set
            {
                if (aerial == null)
                {
                    aerial = value;
                    aerial.AddEventHandlerOnTick(OnTickHandler);
                }
                else
                {
                    throw new AbstractLayerException(AbstractLayerException.ERROR_CODES.LAYER_WAS_ALREADY_SET);
                }
            }
        }
        
        // Abstract
        abstract protected bool Validate(HttpContext httpContext);
        abstract protected object SecurityStrategy();       

        // Helping methods
        private void OnTickHandler(string tick, HttpContext httpContext)
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
            return Aerial.OpenOrder(direction, volume, openPrice, token);
        }
        public AbstractOrder CloseOrder(AbstractOrder order)
        {
            var token = SecurityStrategy();
            return Aerial.CloseOrder(order, token);
        }
        public AbstractOrder OrderStatus(AbstractOrder order)
        {
            var token = SecurityStrategy();
            return Aerial.OrderStatus(order, token);
        }
        public void ChangeExpertAdvisorStateType(ExpertAdvisorStateType state)
        {
            aerial.ChangeExpertAdvisorStateType(state);
        }
        public override bool Equals(object? obj)
        {
            return obj is AbstractSecurity security &&
                   EqualityComparer<Action<string>>.Default.Equals(OnTick, security.OnTick) &&
                   EqualityComparer<AbstractAerial>.Default.Equals(aerial, security.aerial);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(OnTick, aerial);
        }
    }
}
