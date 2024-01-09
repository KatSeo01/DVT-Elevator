using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVT_Elevator
{
    //The Elevator class implements the IElevator interface. It has private fields for the number of floors, current floor, direction, and list of people in the elevator. 
    class Elevator : IElevator
    {
        private int numFloors; // Total number of floors in the building
        private int currentFloor; // Current floor of the elevator
        private int direction; // Direction of movement (1 for up, -1 for down, 0 for idle)
        private List<Person> people; // People currently in the elevator

        public Elevator(int numFloors)
        {
            this.numFloors = numFloors;
            this.currentFloor = 1;
            this.direction = 0;
            this.people = new List<Person>();
        }

        public int CurrentFloor
        {
            get { return this.currentFloor; }
        }

        public void AddPerson(Person person)
        {
            this.people.Add(person); // Adds a person to the elevator
        }

        public async Task MoveToFloorAsync(int targetFloor)
        {
            while (this.currentFloor != targetFloor)
            {
                this.currentFloor += this.direction; // Move one floor in the current direction
                await Task.Delay(100); // Simulate async movement
            }

            // Change direction if necessary
            if (this.currentFloor == 1)
            {
                this.direction = 1;
            }
            else if (this.currentFloor == this.numFloors)
            {
                this.direction = -1; // Start moving down
            }

            // Remove people who have reached their destination
            this.people.RemoveAll(p => p.DestinationFloor == this.currentFloor);
        }
    }
}
