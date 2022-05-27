using CoreApi.Model;
using TestPackages.Bookkeepings;
using TestPackages.Utils.Charts.Ticks;

namespace CoreApi.ExpertAdvisors
{
    public class AbstractTrader
    {        
        // Attributes
        public Guid Id { get; }
        public SessionModel SessionModel { get; }
        public HashSet<BrokerAccount> Brokers { get; }
        public HashSet<ValuePairModel> Pairs { get; } // Traded Pair       

        // Constructor
        public AbstractTrader(SessionModel sessionModel, HashSet<BrokerAccount> brokers, HashSet<ValuePairModel> pairs)
        {
            Id = Guid.NewGuid();
            SessionModel = sessionModel;
            Brokers = brokers;
            Pairs = pairs;
        }

        public void CoreTickHandler(AbstractTick tick)
        {
            throw new NotImplementedException();
        }
    }
}
