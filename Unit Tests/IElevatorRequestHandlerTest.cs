using DVT_Elevator;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorTest
{
    [TestFixture]
    public class ElevatorRequestHandlerTests
    {
        [Test]
        public async Task RequestElevatorAsync_ShouldInvokeMoveToFloorAsync()
        {
            // Arrange
            var elevatorRequestHandler = new MockElevatorRequestHandler();

            // Act
            await elevatorRequestHandler.RequestElevatorAsync(3, 5, 2);

            // Assert
            Assert.IsTrue((elevatorRequestHandler as MockElevatorRequestHandler)?.MoveToFloorAsyncInvoked);
        }

        [Test]

        public void RequestElevatorAsync_ShouldHandleInvalidFloor()
        {
            // Arrange
            var elevatorFactoryMock = new Mock<IElevatorFactory>();
            var building = new Building(10, elevatorFactoryMock.Object);

            // Act and Assert
            var exception = Assert.Throws<ArgumentOutOfRangeException>(
                () => building.RequestElevatorAsync(0, 5, 2).GetAwaiter().GetResult()
            );

            // Assert
            StringAssert.Contains("Invalid floor number", exception.Message);
        }


        [Test]
        public async Task RequestElevatorAsync_ShouldHandleInvalidPassengerCount()
        {
            // Arrange
            var elevatorRequestHandler = new MockElevatorRequestHandler();

            // Act & Assert
            await Task.CompletedTask; 

            Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () =>
                await elevatorRequestHandler.RequestElevatorAsync(3, 5, 0));

            Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () =>
                await elevatorRequestHandler.RequestElevatorAsync(3, 5, -2));
        }


        // MockElevatorRequestHandler class implementing IElevatorRequestHandler for testing purposes
        private class MockElevatorRequestHandler : IElevatorRequestHandler
        {
            public bool MoveToFloorAsyncInvoked { get; private set; }

            public async Task RequestElevatorAsync(int fromFloor, int toFloor, int numPassengers)
            {
                // Handle invalid floor and passenger count scenarios
                ValidateFloor(fromFloor, nameof(fromFloor));
                ValidateFloor(toFloor, nameof(toFloor));
                ValidatePassengerCount(numPassengers);

                // Invoke MoveToFloorAsync for testing purposes
                await MoveToFloorAsync(toFloor);
                MoveToFloorAsyncInvoked = true;
            }

            private async Task MoveToFloorAsync(int targetFloor)
            {
                // Simulate async movement
                await Task.Delay(100);
            }

            private void ValidateFloor(int floor, string paramName)
            {
                if (floor < 1)
                {
                    throw new ArgumentOutOfRangeException(paramName, "Floor must be greater than or equal to 1.");
                }
            }

            private void ValidatePassengerCount(int numPassengers)
            {
                if (numPassengers <= 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(numPassengers), "Number of passengers must be greater than 0.");
                }
            }
        }
    }

}
