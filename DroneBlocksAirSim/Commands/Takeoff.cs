using System.Collections;

namespace DroneBlocksAirSim.Commands
{
    public class Takeoff: MessagePackCommand
    {
        private readonly MessagePackCommand command;

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

        public MessagePackCommand GetCommand()
        {
            return command;
        }
    }
}
