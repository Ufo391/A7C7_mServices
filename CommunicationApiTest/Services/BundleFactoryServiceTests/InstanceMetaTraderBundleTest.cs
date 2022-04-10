using CommunicationApi.Services;
using System;
using TestPackages.Utils.Enums;
using System.Linq;
using Xunit;

namespace CommunicationApiTest.Services.BundleFactoryServiceTests
{
    public class InstanceMetaTraderBundleTest
    {
        /// <summary>
        /// Tests the instantiation of bundles.
        /// </summary>
        [Fact]
        public void Void_Void()
        {
            // Arrange
            var headId = Guid.NewGuid();
            var expectedState = ExpertAdvisorStateType.Initializing;
            var factory = new BundleFactoryService();

            // Act
            var bundleId = factory.InstanceMetaTraderBundle(headId, expectedState);  
            var bundle = factory.GetBundle(bundleId);

            // Assert
            Assert.True(factory.GetAllInstancedBundles().Count() == 1);
            Assert.Equal(expectedState, bundle.State);
            Assert.Equal(headId, bundle.HeadId);
        }
    }
}
