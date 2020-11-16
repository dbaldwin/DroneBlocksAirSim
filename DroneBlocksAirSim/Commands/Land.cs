using System.Collections;

namespace DroneBlocksAirSim.Commands
{
    class Land
    {

        private MessagePackCommand command;

        public Land()
        {
            command = new MessagePackCommand
            {
                Request = 0,
                MessageId = 1,
                Method = "land",
                args = new ArrayList { 60, "" }
            };
        }

        public MessagePackCommand getCommand()
        {
            return command;
        }
    }
}
