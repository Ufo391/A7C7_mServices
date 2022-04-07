using CommunicationApi.Adapters.Abstract;
using TestPackages.Utils.Charts;

namespace CommunicationApiTest.Adapters.Implementation
{
    public class TestReliablilty : AbstractReliability
    {
        public bool ReliabilityStrategyReturn { get; set; }

        protected override bool ReliabilityStrategy()
        {
            return ReliabilityStrategyReturn;
        }

        protected override AbstractTick StringTickToDerivateConversion(string tick)
        {
            return new ForexTick();
        }
    }
}
