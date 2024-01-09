using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVT_Elevator
{
    interface IElevatorMovement
    {
        Task MoveToFloorAsync(int targetFloor);
    }
}
