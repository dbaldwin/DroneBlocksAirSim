using System.Collections;

namespace DroneBlocksAirSim.Commands
{
    class Hover : MessagePackCommand
    {

        private readonly MessagePackCommand command;

        public Hover(float delay)
        {
            command = new MessagePackCommand
            {
                Request = 0,
                MessageId = 1,
                Method = "hover",
                args = new ArrayList { delay*1000 }
            };
        }

        public MessagePackCommand GetCommand()
        {
            return command;
        }
    }
}
