using InterviewApp.Enums;
using InterviewApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewApp
{
    public class Elevator : ElevatorFactory
    {
        private int currentFloor;

        private SortedList<int, string> upDest;
        private SortedList<int, string> downDest;

        private ElevatorDirection currentDirection;

        private ElevatorStatus currentStatus;

        public int id;

        public class descComparer<T> : IComparer<T>
        {
            public int Compare(T x, T y)
            {
                return Comparer<T>.Default.Compare(y, x);
            }
        }
        public Elevator(int elevatorID, int currentFloor)
        {
            this.id = elevatorID;
            this.currentFloor = currentFloor;
            this.currentDirection = ElevatorDirection.ELEVATOR_UP;
            this.currentStatus = ElevatorStatus.READY;
            this.upDest = new SortedList<int, string>();
            this.downDest = new SortedList<int, string>(new descComparer<int>());
        }

        public void addNewDestination(int destination, string displayValue, ElevatorDirection direction)
        {
            if(direction == ElevatorDirection.ELEVATOR_UP)
            {
                upDest.Add(destination, displayValue);
            }
            else if(direction == ElevatorDirection.ELEVATOR_DOWN)
            {
                downDest.Add(destination, displayValue);
            }
            else
            {
                //OpenDoors
            }
        }
        public ElevatorDirection getDirection()
        {        
            return this.currentDirection;
        }

        public ElevatorStatus getStatus()
        {
            return this.currentStatus;
        }
        
        public int getCurrentFloor()
        {
            return currentFloor;
        }

        public void openDoors()
        {
            //open doors
        }

        public void closeDoors()
        {
            //close doors
        }

        public KeyValuePair<int,string> getNextDestination()
        {
            if (currentDirection == ElevatorDirection.ELEVATOR_UP)
            {
                if (upDest.Count != 0)
                {
                    KeyValuePair<int, string> valueToReturn = upDest.First();
                    upDest.RemoveAt(0);
                    return valueToReturn;
                }
                else
                {
                    currentDirection = ElevatorDirection.ELEVATOR_DOWN;
                    return getNextDestination();
                }                
            }
            else
            {
                if(downDest.Count != 0)
                {
                    KeyValuePair<int,string> valueToReturn = downDest.First();
                    downDest.RemoveAt(0);
                    return valueToReturn;
                }                    
                else
                {
                    currentDirection = ElevatorDirection.ELEVATOR_UP;
                    return getNextDestination();
                }
            }
            
        }
    }
}
