using System.Collections;

namespace DroneBlocksAirSim.Commands
{
    class MoveByRollPitchYaw : MessagePackCommand
    {

        private readonly MessagePackCommand command;

        public MoveByRollPitchYaw(float roll, float pitch, float yaw, float duration)
        {
            command = new MessagePackCommand
            {
                Request = 0,
                MessageId = 1,
                Method = "moveByRollPitchYawZ",
                // 4th param is Z and we set it to 0 for now
                args = new ArrayList { roll, pitch, yaw, 0, duration, "" }
            };
        }

        public MessagePackCommand GetCommand()
        {
            return command;
        }


    }
}
