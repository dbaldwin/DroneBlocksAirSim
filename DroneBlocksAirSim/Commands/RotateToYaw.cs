using System.Collections;

namespace DroneBlocksAirSim.Commands
{
    class RotateToYaw : MessagePackCommand
    {

        private readonly MessagePackCommand command;

        public RotateToYaw(float yaw)
        {
            command = new MessagePackCommand
            {
                Request = 0,
                MessageId = 1,
                Method = "rotateToYaw",
                args = new ArrayList { yaw, 600, 5, "" }
            };
        }

        public MessagePackCommand GetCommand()
        {
            return command;
        }
    }
}
