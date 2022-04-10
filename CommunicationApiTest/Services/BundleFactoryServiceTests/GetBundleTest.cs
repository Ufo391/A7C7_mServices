using CommunicationApi.Services;
using System;
using TestPackages.Utils.Enums;
using System.Linq;
using Xunit;
using CommunicationApi.Adapters;

namespace CommunicationApiTest.Services.BundleFactoryServiceTests
{
    public class GetBundleTest
    {
        /// <summary>
        /// Tests whether the bundle is returned as expected.
        /// </summary>
        [Fact]
        public void BundleId_Bundle()
        {
            // Arrange
            var headId = Guid.NewGuid();            
            var factory = new BundleFactoryService();

            // Act & Assert
            Assert.True(factory.GetAllInstancedBundles().Count() == 0);            
            var bundleId = factory.InstanceMetaTraderBundle(headId, ExpertAdvisorStateType.Initializing);            
            var bundle = factory.GetBundle(bundleId);
            Assert.NotNull(bundle);
        }
        /// <summary>
        /// Tests whether null is returned as expected.
        /// </summary>
        [Fact]
        public void BundleId_Null()
        {
            // Arrange                
            var factory = new BundleFactoryService();

            // Act
            var bundle = factory.GetBundle(Guid.NewGuid());

            // Assert                              
            Assert.Null(bundle);
        }
    }
}
