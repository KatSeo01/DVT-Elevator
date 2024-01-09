using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVT_Elevator
{
    //The IElevator interface defines methods: CurrentFloor, and AddPerson
    interface IElevator : IElevatorMovement
    {
        int CurrentFloor { get; }
        Direction CurrentDirection { get; }
        ElevatorState State { get; }
        List<Person> Passengers { get; }
        int PassengerLimit { get; set; }
        void AddPerson(Person person);
        void Update();
    }

    public enum Direction
    {
        Up,
        Down,
        None
    }

    public enum ElevatorState
    {
        Moving,
        Stationary
    }

}
