using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVT_Elevator
{
    //The Person class is responsible for managing a list of people or passengers.
    public class PersonManager
    {
        private List<IPassenger> passengers; // Stores a list of passengers

        public PersonManager()
        {
            this.passengers = new List<IPassenger>(); // Initializes the passenger list
        }

        public void AddPerson(int startFloor, int endFloor, int numPassengers)
        {
            foreach (var _ in Enumerable.Range(0, numPassengers))
            {
                this.passengers.Add(new Person(startFloor, endFloor)); // Adds multiple passengers with the same start and end floors
            }
        }

        public bool HasPeople()
        {
            return this.passengers.Count > 0; // Checks if there are any passengers waiting
        }

        public List<IPassenger> GetPeople()
        {
            return this.passengers.ToList(); // Returns a copy of the passenger list
        }

        public void RemovePerson(IPassenger passenger)
        {
            this.passengers.Remove(passenger); // Removes a specific passenger from the list
        }
    }
}
