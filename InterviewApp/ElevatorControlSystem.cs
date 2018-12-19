using InterviewApp.Enums;
using InterviewApp.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace InterviewApp
{
    public class ElevatorControlSystem : ElevatorControlSystemFactory
    {
        int numberOfElevators = 0;
        int numberOfFloors = 0;
        List<Elevator> elevators;

        public ElevatorControlSystem(int numberOfElevators, int numberOfFloors)
        {
            if (numberOfElevators < 0) throw new Exception("Elevator number must be positive");
            this.numberOfElevators = numberOfElevators;
            this.numberOfFloors = numberOfFloors;
            InitializeElevators();
        }

        private void InitializeElevators()
        {
            elevators = new List<Elevator>();
            for (int idx = 0; idx < this.numberOfElevators; idx++)
            {
                Elevator e = new Elevator(idx, 1);
                elevators.Add(e);                
            }
        }

        public List<Elevator> getElevators()
        {
            return elevators;
        }       
        
        public void processDestination(int elevatorId, int destinationFloor, string strFloorDisplay, ElevatorDirection requestDirection)
        {
            elevators.Find(i => i.id == elevatorId).addNewDestination(destinationFloor, strFloorDisplay, requestDirection);
        }

        public void processFloorRequest(int floorNum, string floorDisplay, ElevatorDirection requestDirection)
        {

            SortedList<int, int> processListTowards = new SortedList<int, int>();
            SortedList<int, int> processListOpp = new SortedList<int, int>();

            foreach (Elevator e in elevators)
            {
                if(e.getStatus() == ElevatorStatus.READY)
                {
                    if (floorNum == e.getCurrentFloor())
                    {
                        e.openDoors();
                        return;
                    }
                    else if(e.getDirection() == ElevatorDirection.ELEVATOR_HOLD)
                    {
                        e.addNewDestination(floorNum, floorDisplay, requestDirection);
                        return;
                    }
                    else
                    {
                        if (requestDirection == e.getDirection())
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
            if(processListTowards.Count > 0)
            {
                int id = processListTowards.OrderByDescending(i => i.Value).First().Key;
                processDestination(id, floorNum, floorDisplay, requestDirection);
            }
            else
            {
                int id = processListOpp.OrderByDescending(i => i.Value).First().Key;
                processDestination(id, floorNum, floorDisplay, requestDirection);
            }            
        }

        public void processAlarm(int elevatorId)
        {
            MailMessage msg = new MailMessage();
            msg.To.Add("to@test.com");
            msg.From = new MailAddress("from@test.com");
            msg.Subject = "Alert!! Pressed on elevator: " + elevatorId;
            msg.Body = "Emergency button is clicked on elevator: " + elevatorId;
            SmtpClient smtp = new SmtpClient("localhost");
            smtp.Send(msg);
        }
    }
}
