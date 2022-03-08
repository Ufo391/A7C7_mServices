using Communication.FastProtocol.Read;
using ExpertAdvisors.Abstract;

namespace ExpertAdvisors._02_Reliability
{
    public class Re_MetaTrader : AbstractReliability
    {
        // Konstruktor
        public Re_MetaTrader(string tickRegexPattern) : base(tickRegexPattern)
        {

        }

        protected override bool ReliabilityStrategy()
        {
            throw new System.NotImplementedException();
        }
    }
}
