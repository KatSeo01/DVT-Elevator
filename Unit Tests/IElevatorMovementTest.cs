using DVT_Elevator;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorTest
{
    [TestFixture]
    public class ElevatorMovementTests
    {
        [Test]
        public async Task MoveToFloorAsync_ShouldChangeCurrentFloor()
        {
            // Arrange
            IElevatorMovement elevatorMovement = new MockElevatorMovement();

            // Act
            await elevatorMovement.MoveToFloorAsync(5);

            // Assert
            Assert.AreEqual(5, (elevatorMovement as MockElevatorMovement)?.CurrentFloor);
        }

        [Test]
        public async Task MoveToFloorAsync_ShouldSimulateAsyncMovement()
        {
            // Arrange
            IElevatorMovement elevatorMovement = new MockElevatorMovement();

            // Act
            var stopwatch = Stopwatch.StartNew();
            await elevatorMovement.MoveToFloorAsync(5);
            stopwatch.Stop();

            // Assert
            Assert.IsTrue(stopwatch.ElapsedMilliseconds >= 100); // Assuming the delay is at least 100 ms
        }

        // MockElevatorMovement class implementing IElevatorMovement for testing purposes
        private class MockElevatorMovement : IElevatorMovement
        {
            public int CurrentFloor { get; private set; }

            public async Task MoveToFloorAsync(int targetFloor)
            {
                // Simulate async movement
                await Task.Delay(100);

                // Update the current floor
                CurrentFloor = targetFloor;
            }
        }
    }

}
