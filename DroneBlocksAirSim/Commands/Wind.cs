using System.Collections;
using System.Collections.Generic;

namespace DroneBlocksAirSim.Commands
{
    class Wind: MessagePackCommand
    {

        private readonly MessagePackCommand command;

        // x is north, y is east, z is down
        public Wind(int x_vel, int y_vel, int z_vel)
        {
            command = new MessagePackCommand
            {
                Request = 0,
                MessageId = 1,
                Method = "simSetWind",
                args = new ArrayList { 
                    new Dictionary<string, int>()
                    {
                        { "x_val", x_vel },
                        { "y_val", y_vel },
                        { "z_val", z_vel }
                    }
                }
            };
        }

        public MessagePackCommand GetCommand()
        {
            return command;
        }

    }
}
