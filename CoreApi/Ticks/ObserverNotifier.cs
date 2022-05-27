using CoreApi.ExpertAdvisors;
using TestPackages.Utils.Charts.Ticks;

namespace CoreApi.Ticks
{
    public class ObserverNotifier
    {
        static private ObserverNotifier singleton;
        static public ObserverNotifier Singleton
        {
            get
            {
                if (singleton is null)
                {
                    singleton = new ObserverNotifier();
                }
                return singleton;
            }
        }

        private readonly Dictionary<Guid, AbstractTrader> Traders;

        public ObserverNotifier()
        {
            Traders = new Dictionary<Guid, AbstractHead>();
        }

        public void ComTickHandler(AbstractTick tick)
        {
            //TODO: Check for existing guid
            CacheTick(tick);
            Traders[tick.TraderHeadId].CoreTickHandler(tick);
        }

        public void AddTrader(AbstractTrader trader)
        {
            Traders[trader.Id] = trader;
        }

        public void RemoveTrader(AbstractTrader trader)
        {
            Traders.Remove(trader.Id);
        }

        public void CacheTick(AbstractTick tick)
        {
            // TODO: Implement logic for caching and save ticks to db 
            throw new NotImplementedException();
        }
    }
}
