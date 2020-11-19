using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneBlocksAirSim.Commands
{
    class MoveByVelocity
    {

        private readonly MessagePackCommand command;

        public MoveByVelocity(int vx, int vy, int vz, int duration)
        {
            command = new MessagePackCommand
            {
                Request = 0,
                MessageId = 1,
                Method = "moveByVelocity",
                // x y z velocity
                args = new ArrayList { vx, vy, vz, duration, 0, new YawMode { is_rate = false, yaw_or_rate = 0 }, ""}
            };
        }

        public MessagePackCommand GetCommand()
        {
            return command;
        }


    }
}
