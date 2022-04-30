using CommunicationApi.Adapters;
using CommunicationApiTest.Adapters.Implementation;
using System;
using TestPackages.Utils.Enums;
using Xunit;
using static TestPackages.Bookkeepings.AbstractOrder;

namespace CommunicationApiTest.Adapters.StackTests.Sending
{
    public class OpenOrderTest
    {
        /// <summary>
        /// Tests whether the function of the lowest level is triggered.
        /// </summary>
        [Fact]
        public void DirectionVolumeOpenPriceToken_Order()
        {
            // Arrange
            var aerial = new TestAerial();
            var security = new TestSecurity();
            var reliability = new TestReliablilty();
            var bundle = new Bundle(
                BundleTypes.MetaTrader, Guid.NewGuid(), ExpertAdvisorStateType.Initializing,
                aerial, security, reliability
                );

            // Act
            reliability.ReliabilityStrategyReturn = true;
            var order = bundle.OpenOrder(DirectionType.BEARISCH, 1f, 1.2f);

            // Assert
            Assert.True(aerial.IsOrderOpenedRequested);
            Assert.Equal(ORDER_STATUS.OPEN, order.Status);
        }
    }
}
