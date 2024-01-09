using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVT_Elevator
{
    //The IElevator interface defines methods: CurrentFloor, and AddPerson
    interface IElevator
    {
        int CurrentFloor { get; }
        void AddPerson(Person person);
    }
}
