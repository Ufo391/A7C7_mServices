

using CommunicationApiTest.Adapters.Implementation;
using Xunit;

namespace CommunicationApiTest.Adapters.AerialTests
{
    public class OnStopHandlerTest
    {
        /// <summary>
        /// Checks whether the inner state of the Test Aerial class is set.
        /// </summary>
        [Fact]
        public void StateInitializing_StopState()
        {
            // Arrange
            var aerial = new TestAerial();

            // Act
            aerial.ChangeExpertAdvisorStateType(TestPackages.Utils.Enums.ExpertAdvisorStateType.Initializing);

            // Assert
            Assert.Equal(TestAerial.InternalStateType.Stop, aerial.InternalState);
        }

        /// <summary>
        /// Checks whether the inner state of the Test Aerial class is set.
        /// </summary>
        [Fact]
        public void StatePaused_StopState()
        {
            // Arrange
            var aerial = new TestAerial();

            // Act
            aerial.ChangeExpertAdvisorStateType(TestPackages.Utils.Enums.ExpertAdvisorStateType.Paused);

            // Assert
            Assert.Equal(TestAerial.InternalStateType.Stop, aerial.InternalState);
        }

        /// <summary>
        /// Checks whether the inner state of the Test Aerial class is set.
        /// </summary>
        [Fact]
        public void StateEmergencyStop_StopState()
        {
            // Arrange
            var aerial = new TestAerial();

            // Act
            aerial.ChangeExpertAdvisorStateType(TestPackages.Utils.Enums.ExpertAdvisorStateType.EmergencyStop);

            // Assert
            Assert.Equal(TestAerial.InternalStateType.Stop, aerial.InternalState);
        }
    }
}
