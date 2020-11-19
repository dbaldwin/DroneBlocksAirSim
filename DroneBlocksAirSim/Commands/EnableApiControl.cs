using System.Collections;

namespace DroneBlocksAirSim.Commands
{
    class EnableApiControl
    {

        private readonly MessagePackCommand command;

        public EnableApiControl()
        {
            command = new MessagePackCommand
            {
                Request = 0,
                MessageId = 0,
                Method = "enableApiControl",
                args = new ArrayList { true, "" }
            };
        }

        public MessagePackCommand GetCommand()
        {
            return command;
        }
    }
}
