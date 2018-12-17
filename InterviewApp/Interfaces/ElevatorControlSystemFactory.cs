using InterviewApp.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewApp.Interfaces
{
    public interface ElevatorControlSystemFactory
    {
        void processRequest(int floorNum, string floorDisplay, ElevatorDirection requestDirection);

        void processDestination(int elevatorId, int destinationFloor, string strFloorNum, ElevatorDirection requestDirection);
    }
}
