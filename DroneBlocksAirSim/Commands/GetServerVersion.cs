using System.Collections;

namespace DroneBlocksAirSim.Commands
{
    class GetServerVersion
    {

        private readonly MessagePackCommand command;

        public GetServerVersion()
        {
            command = new MessagePackCommand
            {
                Request = 0,
                MessageId = 0,
                Method = "getServerVersion",
                args = new ArrayList {}
            };
        }

        public MessagePackCommand GetCommand()
        {
            return command;
        }
    }
}
