using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DroneBlocksAirSim
{
    class MissionHandler
    {

        private MissionStatus missionStatus;

        public MissionHandler(MissionStatus missionStatus)
        {
            this.missionStatus = missionStatus;
        }

        public void StartMissionLoop(ArrayList commands)
        {

            TCP client = new TCP();

            foreach (MessagePackCommand command in commands)
            {

                if (command.Method == "hover")
                {
                    int delay = Convert.ToInt32(command.args[0]);
                    Debug.WriteLine("Delaying for {0}", delay);
                    Thread.Sleep(delay);
                }
                else
                {
                    client.Send(command, 128);
                }
                
                // This may be unnecessary between commands
                Thread.Sleep(500);
            }

            client.Close();
        }
    }
}