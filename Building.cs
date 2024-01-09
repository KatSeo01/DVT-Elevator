using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVT_Elevator
{
    //The Building class represents a building with elevators. It has a list of elevators and a Person object to manage the people waiting for elevators. 
    //The Building class has methods to add a person to the Person, check if there are people waiting for elevators, and update the state of the elevators and people.

    class Building : IElevatorRequestHandler
    {
        private List<IElevator> elevators; // Stores a collection of elevators
        private PersonManager personManager; // Manages people waiting for elevators
        private int numFloors; // Total number of floors in the building
        private IElevatorFactory elevatorFactory; // Creates elevator instances

        public Building(int numFloors, IElevatorFactory elevatorFactory)
        {
            this.numFloors = numFloors;
            this.elevators = Enumerable.Range(0, 2) // Creating 2 elevators
                                       .Select(_ => elevatorFactory.CreateElevator(numFloors))
                                       .ToList();
            this.personManager = new PersonManager();
            this.elevatorFactory = elevatorFactory;
        }

        // Adding people to the waiting list.
        public void AddPerson(int startFloor, int endFloor, int numPassengers)
        {
            this.personManager.AddPerson(startFloor, endFloor, numPassengers);
        }

        //Checking if there are people waiting for elevators.
        public bool HasPeople()
        {
            return this.personManager.HasPeople();
        }

        //Handling a request for an elevator.
        public async Task RequestElevatorAsync(int fromFloor, int toFloor, int numPassengers)
        {
            ValidateFloor(fromFloor, nameof(fromFloor));

            var nearestElevator = FindNearestElevator(fromFloor);
            await AddPassengersAndMoveAsync(nearestElevator, fromFloor, toFloor, numPassengers);
        }

        public void Update()
        {
            // Update each elevator
            foreach (IElevator elevator in this.elevators)
            {
                elevator.Update();
            }

            // Assign people to elevators
            foreach (Person person in this.personManager.GetPeople())
            {
                IElevator closestElevator = FindNearestElevator(person.CurrentFloor);
                closestElevator.AddPerson(person);
                this.personManager.RemovePerson(person);
            }
        }

        // Adds people to the elevator and moves it to the destination floor.
        private async Task AddPassengersAndMoveAsync(IElevator elevator, int fromFloor, int toFloor, int numPassengers)
        {
            foreach (var _ in Enumerable.Range(0, numPassengers))
            {
                this.personManager.AddPerson(fromFloor, toFloor);
            }

            await elevator.MoveToFloorAsync(toFloor);
        }

        //Finds the elevator that's closest to the specified floor
        private IElevator FindNearestElevator(int fromFloor)
        {
            return this.elevators.OrderBy(elevator => Math.Abs(elevator.CurrentFloor - fromFloor)).First();
        }

        //Ensures that a given floor number is within the valid range of floors in the building.
        private void ValidateFloor(int floor, string paramName)
        {
            if (floor < 1 || floor > this.numFloors)
            {
                throw new ArgumentOutOfRangeException(paramName, "Invalid floor number");
            }
        }
    }

}
