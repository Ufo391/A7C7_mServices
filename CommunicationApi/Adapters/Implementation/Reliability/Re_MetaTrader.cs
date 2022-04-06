using CommunicationApi.Adapters.Abstract;
using TestPackages.Utils.Charts;

namespace CommunicationApi.Adapters.Implementation.Reliability
{
    public class Re_MetaTrader : AbstractReliability
    {
        protected override bool ReliabilityStrategy()
        {
            throw new NotImplementedException();
        }

        protected override AbstractTick StringTickToDerivateConversion(string tick)
        {
            throw new NotImplementedException();
        }
    }
}
