using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVT_Elevator
{
    //Creates a new Elevator instance
    public class ElevatorFactory : IElevatorFactory
    {
        public IElevator CreateElevator(int numFloors)
        {
            return new Elevator(numFloors);
        }
    }


}
