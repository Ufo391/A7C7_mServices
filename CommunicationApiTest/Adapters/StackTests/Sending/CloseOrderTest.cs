using CommunicationApi.Adapters;
using CommunicationApiTest.Adapters.Implementation;
using Moq;
using System;
using TestPackages.Bookkeepings;
using TestPackages.Utils.Enums;
using Xunit;

namespace CommunicationApiTest.Adapters.StackTests.Sending
{
    public class CloseOrderTest
    {
        /// <summary>
        /// Tests whether the function of the lowest level is triggered.
        /// </summary>
        [Fact]
        public void Order_Order()
        {
            // Arrange
            var order = new Mock<AbstractOrder>().Object;
            var aerial = new TestAerial();
            var security = new TestSecurity();
            var reliability = new TestReliablilty();
            var bundle = new Bundle(
                BundleTypes.MetaTrader, Guid.NewGuid(), ExpertAdvisorStateType.Initializing,
                aerial, security, reliability
                );

            // Act
            reliability.ReliabilityStrategyReturn = true;
            bundle.CloseOrder(order);

            // Assert
            Assert.True(aerial.IsOrderClosedRequested);
        }
    }
}
