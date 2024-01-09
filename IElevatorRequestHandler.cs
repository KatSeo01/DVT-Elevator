using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVT_Elevator
{
    interface IElevatorRequestHandler
    {
        Task RequestElevatorAsync(int fromFloor, int toFloor, int numPassengers);
    }
}
