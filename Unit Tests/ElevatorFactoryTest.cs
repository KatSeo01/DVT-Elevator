using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVT_Elevator;

namespace ElevatorTest
{
    [TestFixture]
    public class ElevatorFactoryTests
    {
        [Test]
        public void CreateElevator_ShouldReturnIElevatorInstance()
        {
            // Arrange
            var elevatorFactory = new ElevatorFactory();

            // Act
            var elevator = elevatorFactory.CreateElevator(5);

            // Assert
            Assert.IsNotNull(elevator);
            Assert.IsInstanceOf<IElevator>(elevator);
        }

        [Test]
        public void CreateElevator_WithDifferentNumFloors_ShouldReturnIElevatorWithCorrectNumFloors()
        {
            // Arrange
            var elevatorFactory = new ElevatorFactory();

            // Act
            var elevator1 = elevatorFactory.CreateElevator(5);
            var elevator2 = elevatorFactory.CreateElevator(10);

            // Assert
            Assert.That(GetElevatorNumFloors(elevator1), Is.EqualTo(5));
            Assert.That(GetElevatorNumFloors(elevator2), Is.EqualTo(10));

        }

        [Test]
        public void CreateElevator_ShouldReturnElevatorInstance()
        {
            // Arrange
            var elevatorFactory = new ElevatorFactory();

            // Act
            var elevator = elevatorFactory.CreateElevator(10);

            // Assert
            Assert.That(elevator, Is.Not.Null);
            Assert.That(elevator, Is.TypeOf<Elevator>());
            Assert.That(elevator.CurrentFloor, Is.EqualTo(1));
            Assert.That(elevator.CurrentDirection, Is.EqualTo(Direction.None));
            Assert.That(elevator.State, Is.EqualTo(ElevatorState.Stationary));
            Assert.That(elevator.Passengers, Is.Not.Null.And.Empty);
            Assert.That(elevator.PassengerLimit, Is.EqualTo(10));
        }

        // Helper method to get the number of floors from an IElevator instance
        private int GetElevatorNumFloors(IElevator elevator)
        {
            return (elevator as Elevator)?.NumFloors ?? 0;
        }
    }

}
