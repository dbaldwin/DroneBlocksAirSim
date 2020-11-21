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
        public MissionHandler()
        {
        }

        public void StartMissionLoop(ArrayList commands)
        {

            TCP client = new TCP();

            foreach (MessagePackCommand command in commands)
            {
                client.Send(command);
                Thread.Sleep(3000);
            }

            client.Close();
        }
    }
}