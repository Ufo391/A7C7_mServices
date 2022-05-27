using CoreApi.Model;

namespace CoreApi.Services
{
    public interface ITraderFactoryService
    {        
        public void InstanceTrader(SessionModel sessionModel, HashSet<BrokerAccount> brokers, HashSet<ValuePairModel> pairs);
    }
}
