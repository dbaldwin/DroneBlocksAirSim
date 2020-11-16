using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneBlocksAirSim.Commands
{
    class FlyForward
    {
        private MessagePackCommand command;

        // Need to figure out how to use current xyz as reference for moveToPosition
        public FlyForward(int distance)
        {
            command = new MessagePackCommand
            {
                Request = 0,
                MessageId = 1,
                Method = "moveToPosition",
                // x y z velocity
                args = new ArrayList { distance, 0, -10, 5, 60, 0, new YawMode { is_rate = false, yaw_or_rate = 0 }, -1, 1, "" }
            };
        }

        public MessagePackCommand getCommand()
        {
            return command;
        }
    }
}
