using CommunicationApi.Services;
using System;
using TestPackages.Utils.Enums;
using System.Linq;
using Xunit;

namespace CommunicationApiTest.Services.BundleFactoryServiceTests
{
    public class GetAllInstancedBundlesTest
    {
        /// <summary>
        /// Tests whether the expected amount of bundles is returned.
        /// </summary>
        [Fact]
        public void Void_Void()
        {
            // Arrange
            var headId = Guid.NewGuid();            
            var factory = new BundleFactoryService();

            // Act
            factory.InstanceMetaTraderBundle(headId, ExpertAdvisorStateType.Initializing);
            factory.InstanceMetaTraderBundle(headId, ExpertAdvisorStateType.Initializing);

            // Assert
            Assert.True(factory.GetAllInstancedBundles().Count() == 2);
        }
    }
}
