
using CommunicationApi.Adapters;
using CommunicationApiTest.Adapters.Implementation;
using Microsoft.AspNetCore.Http;
using Moq;
using System;
using TestPackages.Utils.Charts.Ticks;
using TestPackages.Utils.Enums;
using Xunit;

namespace CommunicationApiTest.Adapters.StackTests.Receive
{
    public class OnTickTests
    {
        [Fact]
        public void Tick_Success()
        {
            // Arrange
            var aer = new TestAerial();
            var sec = new TestSecurity 
            { 
                ValidateReturn = true 
            };
            var rel = new TestReliablilty()
            {
                ReliabilityStrategyReturn = true                
            };
            var headId = Guid.NewGuid();
            var bundle = new Bundle(BundleTypes.XUnit, headId, 
                ExpertAdvisorStateType.Initializing, 
                aer, sec, rel);
            AbstractTick? bundleTick = null;
            bundle.AddEventHandlerOnTick(tick => 
            {
                bundleTick = tick;
            });
            var context = new Mock<HttpContext>().Object;
            var tickContent = "<test>";

            // Act
            aer.ReceiveTick(tickContent, context);

            // Assert
            Assert.NotNull(bundleTick);
            Assert.Equal(tickContent, bundleTick.TickRaw);
        }
    }
}
