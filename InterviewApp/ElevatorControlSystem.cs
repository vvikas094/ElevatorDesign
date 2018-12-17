using InterviewApp.Enums;
using InterviewApp.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewApp
{
    public class ElevatorControlSystem : ElevatorControlSystemFactory
    {
        public static int MAX_ELEVATORS = 16;
        int numberOfElevators = 0;
        int numberOfFloors = 0;
        List<Elevator> elevators;

        public ElevatorControlSystem(int numberOfElevators, int numberOfFloors)
        {
            if (numberOfElevators < 0) throw new Exception("Elevator number must be positive");
            this.numberOfElevators = (numberOfElevators > MAX_ELEVATORS) ? MAX_ELEVATORS : numberOfElevators;
            this.numberOfFloors = numberOfFloors;
            InitializeElevators();
        }

        private void InitializeElevators()
        {
            elevators = new List<Elevator>();
            for (int idx = 0; idx < this.numberOfElevators; idx++)
            {
                elevators.Add(new Elevator(idx,1));
            }
        }

        public List<Elevator> getElevators()
        {
            return elevators;
        }       
        
        public void processDestination(int elevatorId, int destinationFloor, string strFloorNum, ElevatorDirection requestDirection)
        {
            elevators.Find(i => i.id == elevatorId).addNewDestination(destinationFloor, strFloorNum, requestDirection);
        }

        public void processRequest(int floorNum, string floorDisplay, ElevatorDirection requestDirection)
        {

            SortedList<int, int> processListTowards = new SortedList<int, int>();
            SortedList<int, int> processListOpp = new SortedList<int, int>();

            foreach (Elevator e in elevators)
            {
                if(floorNum == e.getCurrentFloor())
                {
                    e.openDoors();
                    return;
                }
                else
                {
                    if(requestDirection == e.getDirection())
                    {
                        processListTowards.Add(e.id, Math.Abs(e.getCurrentFloor() - floorNum));
                    }
                    else
                    {
                        processListOpp.Add(e.id, Math.Abs(e.getCurrentFloor() - floorNum));
                    }
                }
            }


        }
    }
}
