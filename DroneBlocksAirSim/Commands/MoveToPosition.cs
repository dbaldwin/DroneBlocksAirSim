using System.Collections;

namespace DroneBlocksAirSim.Commands
{
    class MoveToPosition : MessagePackCommand
    {
        private readonly MessagePackCommand command;

        // Need to figure out how to use current xyz as reference for moveToPosition
        public MoveToPosition(float xposition, float yposition, float zposition)
        {
            command = new MessagePackCommand
            {
                Request = 0,
                MessageId = 1,
                Method = "moveToPosition",
                // Setting timeout to 10 minutes for now
                args = new ArrayList { xposition, yposition, zposition, 5, 600, 0, new YawMode { is_rate = false, yaw_or_rate = 0 }, -1, 1, "" }
            };
        }

        public MessagePackCommand GetCommand()
        {
            return command;
        }
    }
}
