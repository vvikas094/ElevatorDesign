using InterviewApp.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewApp.Interfaces
{
    public interface ElevatorFactory
    {
        void addNewDestination(int destination, string DisplayValue, ElevatorDirection direction);
        ElevatorDirection getDirection();
        ElevatorStatus getStatus();
        int getCurrentFloor();

    }
}
