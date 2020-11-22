using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneBlocksAirSim.Commands
{
    class MultirotorState: MessagePackCommand
    {

        private readonly MessagePackCommand command;

        public MultirotorState()
        {
            command = new MessagePackCommand()
            {
                Request = 0,
                MessageId = 1,
                Method = "getMultirotorState",
                args = new ArrayList { "" }
            };
        }

        public MessagePackCommand GetCommand()
        {
            return command;
        }
    }
}
