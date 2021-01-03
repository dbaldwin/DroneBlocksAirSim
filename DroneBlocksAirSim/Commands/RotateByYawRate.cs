using System.Collections;

namespace DroneBlocksAirSim.Commands
{
    class RotateByYawRate : MessagePackCommand
    {

        private readonly MessagePackCommand command;

        public RotateByYawRate(float yawRate, float duration)
        {
            command = new MessagePackCommand
            {
                Request = 0,
                MessageId = 1,
                Method = "rotateByYawRate",
                args = new ArrayList { yawRate, duration, ""}
            };
        }

        public MessagePackCommand GetCommand()
        {
            return command;
        }
    }
}
