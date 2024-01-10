using DVT_Elevator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorTest
{
    [TestFixture]
    public class PersonManagerTests
    {
        [Test]
        public void AddPerson_ShouldAddMultiplePassengersWithSameStartAndEndFloors()
        {
            // Arrange
            var personManager = new PersonManager();

            // Act
            personManager.AddPerson(3, 5, 3);

            // Assert
            var people = personManager.GetPeople();
            Assert.That(people.Count, Is.EqualTo(3));
            Assert.IsTrue(people.All(p => p.CurrentFloor == 3 && p.DestinationFloor == 5));
        }

        [Test]
        public void HasPeople_ShouldReturnTrueWhenPeopleExist()
        {
            // Arrange
            var personManager = new PersonManager();
            personManager.AddPerson(2, 4, 1);

            // Act
            var hasPeople = personManager.HasPeople();

            // Assert
            Assert.IsTrue(hasPeople);
        }

        [Test]
        public void HasPeople_ShouldReturnFalseWhenNoPeopleExist()
        {
            // Arrange
            var personManager = new PersonManager();

            // Act
            var hasPeople = personManager.HasPeople();

            // Assert
            Assert.IsFalse(hasPeople);
        }

        [Test]
        public void GetPeople_ShouldReturnCopyOfPassengerList()
        {
            // Arrange
            var personManager = new PersonManager();
            personManager.AddPerson(1, 3, 2);

            // Act
            var people = personManager.GetPeople();

            // Assert
            Assert.That(people.Count, Is.EqualTo(2));
            Assert.That(personManager.GetPeople(), Is.Not.SameAs(people));

        }

        [Test]
        public void RemovePerson_ShouldRemoveSpecificPassengerFromList()
        {
            // Arrange
            var personManager = new PersonManager();
            var passengerToRemove = new Person(4, 6);
            personManager.AddPerson(4, 6, 1);

            // Act
            personManager.RemovePerson(passengerToRemove);

            // Assert
            var people = personManager.GetPeople();
            Assert.That(people.Count, Is.EqualTo(0));

        }
    }

}
