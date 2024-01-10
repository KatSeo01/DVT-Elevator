using DVT_Elevator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorTest
{
    [TestFixture]
    public class PersonTests
    {
        [Test]
        public void CurrentFloor_ShouldReturnStartFloor()
        {
            // Arrange
            var person = new Person(3, 5);

            // Act
            var currentFloor = person.CurrentFloor;

            // Assert
            Assert.AreEqual(3, currentFloor);
        }

        [Test]
        public void DestinationFloor_ShouldReturnDestinationFloor()
        {
            // Arrange
            var person = new Person(2, 4);

            // Act
            var destinationFloor = person.DestinationFloor;

            // Assert
            Assert.AreEqual(4, destinationFloor);
        }

        [Test]
        public void HasReachedDestination_ShouldReturnTrueWhenStartFloorEqualsDestinationFloor()
        {
            // Arrange
            var person = new Person(3, 3);

            // Act
            var hasReachedDestination = person.HasReachedDestination();

            // Assert
            Assert.IsTrue(hasReachedDestination);
        }

        [Test]
        public void HasReachedDestination_ShouldReturnFalseWhenStartFloorDoesNotEqualDestinationFloor()
        {
            // Arrange
            var person = new Person(2, 5);

            // Act
            var hasReachedDestination = person.HasReachedDestination();

            // Assert
            Assert.IsFalse(hasReachedDestination);
        }
    }

}
