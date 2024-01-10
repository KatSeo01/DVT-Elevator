using Moq;
using DVT_Elevator;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace ElevatorTest
{
    class BuildingTests
    {
        private Mock<IElevatorFactory> elevatorFactoryMock;

        [SetUp]
        public void Setup()
        {
            elevatorFactoryMock = new Mock<IElevatorFactory>();
            elevatorFactoryMock.Setup(factory => factory.CreateElevator(It.IsAny<int>()))
                               .Returns(Mock.Of<IElevator>());
        }

        [Test]
        public void AddPerson_AddsPersonToPersonManager()
        {
            // Arrange
            var building = new Building(5, elevatorFactoryMock.Object);

            // Act
            building.AddPerson(1, 3, 2);

            // Assert
            Assert.IsTrue(building.HasPeople());
        }

        [Test]
        public async Task RequestElevatorAsync_ValidRequest_CallsMoveToFloorAsync()
        {
            // Arrange
            var elevatorMock = new Mock<IElevator>();
            elevatorFactoryMock.Setup(factory => factory.CreateElevator(It.IsAny<int>()))
                               .Returns(elevatorMock.Object);

            var building = new Building(5, elevatorFactoryMock.Object);

            // Act
            await building.RequestElevatorAsync(1, 3, 2);

            // Assert
            elevatorMock.Verify(elevator => elevator.MoveToFloorAsync(3), Times.Once);
        }

        [Test]
        public void DisplayElevatorStatus_PrintsElevatorStatusToConsole()
        {
            // Arrange
            var elevatorMock = new Mock<IElevator>();
            elevatorMock.Setup(elevator => elevator.Passengers).Returns(new List<Person>());
            elevatorMock.Setup(elevator => elevator.CurrentFloor).Returns(2); // Set the desired CurrentFloor
            elevatorMock.Setup(elevator => elevator.CurrentDirection).Returns(Direction.Up);
            elevatorMock.Setup(elevator => elevator.State).Returns(ElevatorState.Idle);

            elevatorFactoryMock.Setup(factory => factory.CreateElevator(It.IsAny<int>()))
                               .Returns(elevatorMock.Object);

            var building = new Building(5, elevatorFactoryMock.Object);

            // Act
            building.Update(); // Make sure Update is called to update elevator status
            building.DisplayElevatorStatus();
            Console.WriteLine(GetConsoleOutput());

            // Assert
            StringAssert.Contains("Elevator at Floor 2, Direction: Up, State: Idle, Passengers: 0", GetConsoleOutput());
        }

        private string GetConsoleOutput()
        {
            using (var consoleOutput = new ConsoleOutput())
            {
                // Intentionally empty; used to capture Console.WriteLine output
                return consoleOutput.GetOutput();
            }

        }


        [Test]
        public void Update_CallsUpdateOnEachElevatorAndAddsPeopleToNearestElevator()
        {
            // Arrange
            var elevatorMock = new Mock<IElevator>();
            elevatorFactoryMock.Setup(factory => factory.CreateElevator(It.IsAny<int>()))
                               .Returns(elevatorMock.Object);

            var building = new Building(5, elevatorFactoryMock.Object);
            building.AddPerson(1, 3, 2);

            // Act
            building.Update();

            // Assert
            elevatorMock.Verify(elevator => elevator.Update(), Times.Exactly(2));
            elevatorMock.Verify(elevator => elevator.AddPerson(It.IsAny<Person>()), Times.Exactly(2));
        }

        [Test]
        public void ValidateFloor_InvalidFloor_ThrowsArgumentOutOfRangeException()
        {
            // Arrange
            var building = new Building(5, elevatorFactoryMock.Object);

            // Act and Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => building.ValidateFloor(0, "floor"));
            Assert.Throws<ArgumentOutOfRangeException>(() => building.ValidateFloor(6, "floor"));
        }

        
    }

    // Helper class to capture Console.WriteLine output
    public class ConsoleOutput : IDisposable
    {
        private readonly StringWriter stringWriter;
        private readonly TextWriter originalOutput;

        public ConsoleOutput()
        {
            stringWriter = new StringWriter();
            originalOutput = Console.Out;
            Console.SetOut(stringWriter);
        }

        public string GetOutput()
        {
            return stringWriter.ToString();
        }

        public void Dispose()
        {
            Console.SetOut(originalOutput);
            stringWriter.Dispose();
        }
    }

}