using DVT_Elevator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ElevatorTest
{
    [TestFixture]
    public class IElevatorTests
    {
        [Test]
        public void AddPerson_WhenElevatorIsNotFull_ShouldAddPerson()
        {
            // Arrange
            IElevator elevator = new MockElevator();
            var person = new Person(1, 3);

            // Act
            elevator.AddPerson(person);

            // Assert
            Assert.AreEqual(1, elevator.Passengers.Count);
        }

        [Test]
        public void AddPerson_WhenElevatorIsFull_ShouldNotAddPerson()
        {
            // Arrange
            IElevator elevator = new MockElevator();
            elevator.PassengerLimit = 1;

            var person1 = new Person(1, 3);
            var person2 = new Person(2, 4);

            // Act
            elevator.AddPerson(person1);
            elevator.AddPerson(person2);

            // Assert
            Assert.AreEqual(1, elevator.Passengers.Count);
        }

        [Test]
        public void Update_ShouldUpdateElevatorStatus()
        {
            // Arrange
            IElevator elevator = new MockElevator();

            // Act
            elevator.Update();

            // Assert
            // Add assertions based on the expected behavior after an update
        }

        // MockElevator class implementing IElevator for testing purposes
        private class MockElevator : IElevator, IElevatorMovement
        {
            public int CurrentFloor { get; set; }
            public Direction CurrentDirection { get; set; }
            public ElevatorState State { get; set; }
            public List<Person> Passengers { get; } = new List<Person>();
            public int PassengerLimit { get; set; } = 10;

            public void AddPerson(Person person)
            {
                if (Passengers.Count < PassengerLimit)
                {
                    this.Passengers.Add(person);
                }
            }

            public void Update()
            {
                // Implement mock behavior for update
            }

            public async Task MoveToFloorAsync(int targetFloor)
            {
                // Implement mock behavior for moving to a floor asynchronously
                await Task.Delay(100); // Simulate async movement
                this.CurrentFloor = targetFloor;
            }
        }
    }
}
