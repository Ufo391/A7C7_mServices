using CommunicationApi.Services;
using System;
using TestPackages.Utils.Enums;
using Xunit;

namespace CommunicationApiTest.Services.BundleFactoryServiceTests
{
    public class ChangeBundleStateTest
    {
        /// <summary>
        /// Tests whether the state of a bundle is also changed as expected.
        /// </summary>
        [Fact]
        public void BundleIdNewState_Void()
        {
            // Arrange
            var headId = Guid.NewGuid();
            var targetState = ExpertAdvisorStateType.Running;
            var factory = new BundleFactoryService();

            // Act & Assert
            var bundleId = factory.InstanceMetaTraderBundle(headId, ExpertAdvisorStateType.Initializing);
            var bundle = factory.GetBundle(bundleId);
            Assert.False(bundle.State == targetState);
            factory.ChangeBundleState(bundleId, targetState);     
            Assert.True(bundle.State == targetState);
        }
    }
}
