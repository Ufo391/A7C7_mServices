

using CommunicationApiTest.Adapters.Implementation;
using Xunit;

namespace CommunicationApiTest.Adapters.AerialTests
{
    public class OnStartHandlerTest
    {
        /// <summary>
        /// Checks whether the inner state of the Test Aerial class is set.
        /// </summary>
        [Fact]
        public void StateRunning_StartState()
        {
            // Arrange
            var aerial = new TestAerial();

            // Act
            aerial.ChangeExpertAdvisorStateType(TestPackages.Utils.Enums.ExpertAdvisorStateType.Running);

            // Assert
            Assert.Equal(TestAerial.InternalStateType.Start, aerial.InternalState);
        }
    }
}
