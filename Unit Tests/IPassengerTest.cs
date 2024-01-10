using DVT_Elevator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorTest
{
    [TestFixture]
    public class PassengerTests
    {
        [Test]
        public void HasReachedDestination_ShouldReturnTrueWhenCurrentFloorEqualsDestinationFloor()
        {
            // Arrange
            var passenger = new MockPassenger(3, 3);

            // Act
            var hasReachedDestination = passenger.HasReachedDestination();

            // Assert
            Assert.IsTrue(hasReachedDestination);
        }

        [Test]
        public void HasReachedDestination_ShouldReturnFalseWhenCurrentFloorDoesNotEqualDestinationFloor()
        {
            // Arrange
            var passenger = new MockPassenger(2, 5);

            // Act
            var hasReachedDestination = passenger.HasReachedDestination();

            // Assert
            Assert.IsFalse(hasReachedDestination);
        }

        // MockPassenger class implementing IPassenger for testing purposes
        private class MockPassenger : IPassenger
        {
            public int CurrentFloor { get; }
            public int DestinationFloor { get; }

            public MockPassenger(int currentFloor, int destinationFloor)
            {
                CurrentFloor = currentFloor;
                DestinationFloor = destinationFloor;
            }

            public bool HasReachedDestination()
            {
                return CurrentFloor == DestinationFloor;
            }
        }
    }

}
