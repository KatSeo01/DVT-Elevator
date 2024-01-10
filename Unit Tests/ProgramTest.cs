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
    public class ProgramTests
    {
        [Test]
        public async Task Main_ShouldExecuteOptionsAndExit()
        {
            // Arrange
            var fakeUserInput = "2\n3\n"; // Simulate user input "2", "3", and Enter
            var fakeConsoleOutput = new StringWriter();
            Console.SetIn(new StringReader(fakeUserInput));
            Console.SetOut(fakeConsoleOutput);

            try
            {
                // Act
               await Program.Main(Array.Empty<string>()); // Pass an empty string array instead of null


                // Assert
                var actualOutput = fakeConsoleOutput.ToString();
                Assert.That(actualOutput, Does.Contain("Display Elevator Status"));
                Assert.That(actualOutput, Does.Contain("Exiting the Elevator System. Goodbye!"));
            }
            finally
            {
                // Clean up
                Console.SetIn(Console.In);
                Console.SetOut(Console.Out);
            }
        }

        [Test]
        public void RequestElevator_ShouldHandleValidInput()
        {
            // Arrange
            var buildingMock = new Mock<Building>();
            buildingMock.Setup(b => b.RequestElevatorAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()))
                        .Returns(Task.CompletedTask);

            var fakeUserInput = "3\n5\n2\n"; // Simulate user input "3", "5", "2", and Enter
            Console.SetIn(new StringReader(fakeUserInput));

            try
            {
                // Act
                Program.RequestElevator(buildingMock.Object);

                // Assert
                buildingMock.Verify(b => b.RequestElevatorAsync(3, 5, 2), Times.Once);
            }
            finally
            {
                // Clean up
                Console.SetIn(Console.In);
            }
        }

        [Test]
        public void RequestElevator_ShouldHandleInvalidInput()
        {
            // Arrange
            var buildingMock = new Mock<Building>();
            buildingMock.Setup(b => b.RequestElevatorAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()))
                        .Returns(Task.CompletedTask);

            var fakeUserInput = "Invalid\n5\n2\n"; // Simulate user input "Invalid", "5", "2", and Enter
            Console.SetIn(new StringReader(fakeUserInput));

            try
            {
                // Act
                Program.RequestElevator(buildingMock.Object);

                // Assert
                buildingMock.Verify(b => b.RequestElevatorAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()), Times.Never);
            }
            finally
            {
                // Clean up
                Console.SetIn(Console.In);
            }
        }
    }

}
