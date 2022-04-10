using CommunicationApi.Adapters;
using CommunicationApiTest.Adapters.Implementation;
using System;
using TestPackages.Utils.Enums;
using Xunit;

namespace CommunicationApiTest.Adapters.StackTests.Sending
{
    public class ChangeExpertAdvisorStateTypeTest
    {
        /// <summary>
        /// Tests whether the function of the lowest level is triggered.
        /// </summary>
        [Fact]
        public void State_Void()
        {
            // Arrange            
            var aerial = new TestAerial();
            var security = new TestSecurity();
            var reliability = new TestReliablilty();
            var bundle = new Bundle(
                BundleTypes.MetaTrader, Guid.NewGuid(), ExpertAdvisorStateType.Initializing,
                aerial, security, reliability
                );

            // Assert & Act
            reliability.ReliabilityStrategyReturn = true;
            Assert.Equal(TestAerial.InternalStateType.Init, aerial.InternalState);
            bundle.ChangeExpertAdvisorStateType(ExpertAdvisorStateType.Running);
            Assert.Equal(TestAerial.InternalStateType.Start, aerial.InternalState);            

        }
    }
}
