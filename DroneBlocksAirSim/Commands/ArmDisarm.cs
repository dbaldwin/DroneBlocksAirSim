using System.Collections;

namespace DroneBlocksAirSim.Commands
{
    class ArmDisarm
    {

        private readonly MessagePackCommand command;

        public ArmDisarm(bool arm)
        {
            command = new MessagePackCommand
            {
                Request = 0,
                MessageId = 1,
                Method = "armDisarm",
                args = new ArrayList { arm, "" }
            };
        }

        public MessagePackCommand GetCommand()
        {
            return command;
        }
    }
}
