using TestPackages.Bookkeepings;
using TestPackages.Utils.Enums;
using static TestPackages.Bookkeepings.AbstractOrder;

namespace CommunicationApi.Adapters.Abstract
{
    public abstract class AbstractAerial
    {
        // Events
        protected Action<string, HttpContext> OnTick = delegate { };
        
        abstract internal ExpertAdvisorStateType EAStatusRequest();
        abstract internal AbstractOrder OpenOrder(DirectionType direction, double volume, double openPrice, object token);
        abstract internal AbstractOrder CloseOrder(AbstractOrder order, object token);
        abstract internal AbstractOrder OrderStatus(AbstractOrder order, object token);

        internal void AddEventHandlerOnTick(Action<string, HttpContext> meth)
        {
            OnTick += meth;
        }

        public override bool Equals(object? obj)
        {
            return obj is AbstractAerial aerial &&
                   EqualityComparer<Action<string, HttpContext>>.Default.Equals(OnTick, aerial.OnTick);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(OnTick);
        }
    }
}