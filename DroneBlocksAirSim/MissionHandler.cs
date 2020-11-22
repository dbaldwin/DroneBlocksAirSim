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
                client.Send(command, 128);
                Thread.Sleep(500);
            }

            client.Close();
        }
    }
}