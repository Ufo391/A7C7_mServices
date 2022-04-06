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
        abstract internal AbstractOrder OpenOrder(DirectionType direction, double volume, double openPrice, string token);
        abstract internal AbstractOrder CloseOrder(AbstractOrder order, string token);
        abstract internal AbstractOrder OrderStatus(AbstractOrder order, string token);

        internal void AddEventHandlerOnTick(Action<string, HttpContext> meth)
        {
            OnTick += meth;
        }
    }
}
