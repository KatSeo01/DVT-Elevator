using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVT_Elevator
{
    //The Elevator class implements the IElevator interface. It has private fields for the number of floors, current floor, direction, and list of people in the elevator. 
    public class Elevator : IElevator
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
            this.State = ElevatorState.Stationary;
        }

        public int CurrentFloor => this.currentFloor;
        public Direction CurrentDirection => this.direction == 0 ? Direction.None : (this.direction == 1 ? Direction.Up : Direction.Down);
        public ElevatorState State { get; private set; }
        public List<Person> Passengers => this.people;
        public int PassengerLimit { get; set; } = 10;

        // Adds a person to the elevator
        public void AddPerson(Person person)
        {
            if (Passengers.Count < PassengerLimit)
            {
                this.people.Add(person);
                Console.WriteLine($"Passenger added to elevator. Current passengers: {this.people.Count}");
            }
            else
            {
                Console.WriteLine("Elevator is full. Unable to add more passengers.");
            }
        }

        public async Task MoveToFloorAsync(int targetFloor)
        {
            State = ElevatorState.Moving;

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
            State = ElevatorState.Stationary;

            Console.WriteLine($"Elevator is now at Floor {this.currentFloor}, " +
                              $"Direction: {this.CurrentDirection}, " +
                              $"State: {this.State}, " +
                              $"Passengers: {this.Passengers.Count}");
        }

        public void Update()
        {
            // periodic updates for the elevator
            Console.WriteLine($"Elevator at Floor {this.CurrentFloor} is updating its status...");

            UpdateDisplay();

        }

        private void UpdateDisplay()
        {
            // Update display with elevator status
            Console.WriteLine($"Updating display: Floor {this.CurrentFloor}, Direction: {this.CurrentDirection}, State: {this.State}");
        }

    }
}
