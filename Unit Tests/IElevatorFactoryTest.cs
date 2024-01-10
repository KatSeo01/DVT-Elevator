using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVT_Elevator;

namespace ElevatorTest
{
    [TestFixture]
    public class IElevatorFactoryTests
    {
        [Test]
        public void CreateElevator_ShouldReturnIElevatorInstance()
        {
            // Arrange
            IElevatorFactory elevatorFactory = new ElevatorFactory();

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
            IElevatorFactory elevatorFactory = new ElevatorFactory();

            // Act
            var elevator1 = elevatorFactory.CreateElevator(5);
            var elevator2 = elevatorFactory.CreateElevator(10);

            // Assert
            Assert.AreEqual(5, GetElevatorNumFloors(elevator1));
            Assert.AreEqual(10, GetElevatorNumFloors(elevator2));
        }

        // Helper method to get the number of floors from an IElevator instance
        private int GetElevatorNumFloors(IElevator elevator)
        {
            return (elevator as Elevator)?.NumFloors ?? 0;
        }
    }

}
