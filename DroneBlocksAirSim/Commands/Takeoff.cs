
using System.Collections;

namespace DroneBlocksAirSim.Commands
{
    public class Takeoff
    {
        private MessagePackCommand command;

        public Takeoff()
        {
            command = new MessagePackCommand
            {
                Request = 0,
                MessageId = 1,
                Method = "takeoff",
                args = new ArrayList { 5, "" }
            };
        }

        public MessagePackCommand getCommand()
        {
            return command;
        }
    }
}
