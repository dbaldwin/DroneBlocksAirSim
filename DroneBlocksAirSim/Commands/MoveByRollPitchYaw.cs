using System.Collections;
using System;

namespace DroneBlocksAirSim.Commands
{
    class MoveByRollPitchYaw : MessagePackCommand
    {

        private readonly MessagePackCommand command;

        public MoveByRollPitchYaw(float roll, float pitch, float yaw, float duration)
        {

            // RPY will come in as degrees but should be converted to radians per the API
            roll *= (float)(Math.PI/180);

            // These are negative per https://github.com/microsoft/AirSim/blob/master/PythonClient/airsim/client.py#L1028
            pitch *= (float)(Math.PI/180) * -1;
            yaw *= (float)(Math.PI/180) * -1;

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
