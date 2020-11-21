using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneBlocksAirSim.Commands
{
    
    class WeatherEnable: MessagePackCommand
    {
        private readonly MessagePackCommand command;

        public WeatherEnable(bool enable)
        {
            command = new MessagePackCommand
            {
                Request = 0,
                MessageId = 1,
                Method = "simEnableWeather",
                args = new ArrayList { enable }
            };
        }

        public MessagePackCommand GetCommand()
        {
            return command;
        }
    }
}
