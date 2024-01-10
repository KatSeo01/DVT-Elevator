using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVT_Elevator;
using Moq;
using NUnit.Framework.Legacy;

namespace ElevatorTest
{
    [TestFixture]
    public class ElevatorTests
    {
        [Test]
        public void AddPerson_WhenElevatorIsNotFull_ShouldAddPerson()
        {
            // Arrange
            var elevator = new Elevator(5);
            var person = new Person(1, 3);

            // Act
            elevator.AddPerson(person);

            // Assert
            Assert.That(elevator.Passengers.Count, Is.EqualTo(1));

        }

        [Test]
        public void AddPerson_WhenElevatorIsFull_ShouldNotAddPerson()
        {
            // Arrange
            var elevator = new Elevator(5);
            elevator.PassengerLimit = 1;

            var person1 = new Person(1, 3);
            var person2 = new Person(2, 4);

            // Act
            elevator.AddPerson(person1);
            elevator.AddPerson(person2);

            // Assert
            Assert.That(elevator.Passengers.Count, Is.EqualTo(1));

            StringAssert.Contains("Elevator is full. Unable to add more passengers.", GetConsoleOutput());
        }

        [Test]
        public async Task MoveToFloorAsync_ShouldMoveToTargetFloor()
        {
            // Arrange
            var elevator = new Elevator(5);

            // Act
            await elevator.MoveToFloorAsync(3);

            // Assert
            Assert.That(elevator.CurrentFloor, Is.EqualTo(3));
            Assert.That(elevator.CurrentDirection, Is.EqualTo(Direction.Up));
            Assert.That(elevator.State, Is.EqualTo(ElevatorState.Stationary));
            Assert.IsEmpty(elevator.Passengers);
        }

        [Test]
        public void Update_ShouldUpdateElevatorStatus()
        {
            // Arrange
            var elevator = new Elevator(5);

            // Act
            elevator.Update();

            // Assert
            StringAssert.Contains($"Elevator at Floor {elevator.CurrentFloor} is updating its status...", GetConsoleOutput());
            StringAssert.Contains($"Updating display: Floor {elevator.CurrentFloor}, Direction: {elevator.CurrentDirection}, State: {elevator.State}", GetConsoleOutput());
        }

        // Helper method to capture Console.WriteLine output
        private string GetConsoleOutput()
        {
            using (var consoleOutput = new ConsoleOutput())
            {
                return consoleOutput.GetOutput();
            }
        }
    }

}
