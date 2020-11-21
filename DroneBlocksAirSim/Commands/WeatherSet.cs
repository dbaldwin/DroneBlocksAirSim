using System.Collections;

namespace DroneBlocksAirSim.Commands
{
    class WeatherSet: MessagePackCommand
    {
        private readonly MessagePackCommand command;

        public WeatherSet(int weatherType, float intensity)
        {
            command = new MessagePackCommand
            {
                Request = 0,
                MessageId = 0,
                Method = "simSetWeatherParameter",
                args = new ArrayList { weatherType, intensity }
            };
        }

        public MessagePackCommand GetCommand()
        {
            return command;
        }
    }
}
