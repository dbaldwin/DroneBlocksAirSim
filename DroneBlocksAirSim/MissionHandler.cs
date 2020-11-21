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


/*
 *  var enableApiControl = new MessagePackCommand
            {
                Request = 0,
                MessageId = 0,
                Method = "enableApiControl",
                args = new ArrayList { true, "" }
            };

            var arm = new MessagePackCommand
            {
                Request = 0,
                MessageId = 1,
                Method = "armDisarm",
                args = new ArrayList { true, "" }
            };

            var disarm = new MessagePackCommand
            {
                Request = 0,
                MessageId = 1,
                Method = "armDisarm",
                args = new ArrayList { false, "" }
            };
*/