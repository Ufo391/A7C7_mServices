using TestPackages.Bookkeepings;
using TestPackages.Utils.Enums;
using static TestPackages.Bookkeepings.AbstractOrder;

namespace CommunicationApi.Adapters.Abstract
{
    public abstract class AbstractAerial
    {
        // Attributes
        protected Action<string, HttpContext> OnTick = delegate { };
        protected Action OnStop = delegate { };
        protected Action OnStart = delegate { };
        protected ExpertAdvisorStateType State { get; private set; }

        // Constructor
        public AbstractAerial()
        {
            OnStart += OnStartHandler;
            OnStop += OnStopHandler;
        }

        // Abstract
        abstract public ExpertAdvisorStateType EAStatusRequest();
        abstract public AbstractOrder OpenOrder(DirectionType direction, double volume, double openPrice, object token);
        abstract public AbstractOrder CloseOrder(AbstractOrder order, object token);
        abstract public AbstractOrder OrderStatus(AbstractOrder order, object token);
        abstract protected void OnStartHandler();
        abstract protected void OnStopHandler();

        // Helping methods
        public void ChangeExpertAdvisorStateType(ExpertAdvisorStateType state)
        {
            State = state;

            if (state == ExpertAdvisorStateType.Running)
            {                
                OnStart();
            }
            else if(state == ExpertAdvisorStateType.Initializing || state == ExpertAdvisorStateType.EmergencyStop || state == ExpertAdvisorStateType.Paused)
            {
                OnStop();
            }
            else
            {
                throw new NotImplementedException($"Unknown expert advisor state handler! State: {Enum.GetName(typeof(ExpertAdvisorStateType), state)}");
            }            
        }

        internal void AddEventHandlerOnTick(Action<string, HttpContext> meth)
        {
            OnTick += meth;
        }

        public override bool Equals(object? obj)
        {
            return obj is AbstractAerial aerial &&
                   EqualityComparer<Action<string, HttpContext>>.Default.Equals(OnTick, aerial.OnTick) &&
                   EqualityComparer<Action>.Default.Equals(OnStop, aerial.OnStop) &&
                   EqualityComparer<Action>.Default.Equals(OnStart, aerial.OnStart);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(OnTick, OnStop, OnStart);
        }
    }
}