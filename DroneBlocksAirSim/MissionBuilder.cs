using DroneBlocksAirSim.Commands;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneBlocksAirSim
{


    // A basic mission
    // takeoff|fly_forward,20,in|fly_backward,20,in|fly_left,20,in|fly_left,20,in|fly_up,20,in|fly_down,20,in|land

    class MissionBuilder
    {

        private string missionString;

        public MissionBuilder(string missionString)
        {
            this.missionString = missionString;
        }

        public ArrayList parseMission()
        {

            // In some cases when there's a single block we need to remove the leading pipe
            if (missionString.IndexOf("|") == 0)
            {
                missionString = missionString.Remove(0, 1);
            }

            // Initialize the command list
            ArrayList commandList = new ArrayList();

            // Let's enable API control
            // Need to make this more intelligent to determine if drone is in air
            commandList.Add(new EnableApiControl().GetCommand());

            foreach (var commandString in missionString.Split("|"))
            {
                string[] parameters = commandString.Split(",");

                string command = parameters[0];

                if (command.IndexOf("disarm") > -1)
                {
                    commandList.Add(new ArmDisarm(false).GetCommand());
                }
                else if (command.IndexOf("arm") > -1)
                {
                    commandList.Add(new ArmDisarm(true).GetCommand());
                }
                else if (command.IndexOf("takeoff") > -1)
                {
                    commandList.Add(new Takeoff().GetCommand());
                }
                // We can handle these in the same statement because the blocks will send a negative value for backward flight
                else if (command.IndexOf("fly_forward") > -1 || command.IndexOf("fly_backward") > -1)
                {
                    int xvelocity = int.Parse(parameters[1]);
                    int duration = int.Parse(parameters[2]);
                    Debug.WriteLine("fly by x velocity {0} for {1} s", xvelocity, duration);
                    commandList.Add(new MoveByVelocity(xvelocity, 0, 0, duration).GetCommand());
                }
                else if (command.IndexOf("fly_left") > -1 || command.IndexOf("fly_right") > -1)
                {
                    int yvelocity = int.Parse(parameters[1]);
                    int duration = int.Parse(parameters[2]);
                    Debug.WriteLine("fly by y velocity {0} for {1} s", yvelocity, duration);
                    commandList.Add(new MoveByVelocity(0, yvelocity, 0, duration).GetCommand());
                }
                else if (command.IndexOf("fly_up") > -1 || command.IndexOf("fly_down") > -1)
                {
                    int zvelocity = int.Parse(parameters[1]);
                    int duration = int.Parse(parameters[2]);
                    Debug.WriteLine("fly by z velocity {0} for {1} s", zvelocity, duration);
                    commandList.Add(new MoveByVelocity(0, 0, zvelocity, duration).GetCommand());
                }
                else if (command.IndexOf("fly_to_position") > -1)
                {
                    float xposition = float.Parse(parameters[1]);
                    float yposition = float.Parse(parameters[2]);
                    float zposition = float.Parse(parameters[3]);
                    Debug.WriteLine("flying to position {0}, {1}, {2}", xposition, yposition, zposition);
                    commandList.Add(new MoveToPosition(xposition, yposition, zposition).GetCommand());
                }
                else if (command.IndexOf("weather_enable") > -1)
                {
                    bool enable = bool.Parse(parameters[1]);
                    commandList.Add(new WeatherEnable(enable).GetCommand());
                }
                else if (command.IndexOf("weather_set") > -1)
                {
                    int weatherType = int.Parse(parameters[1]);
                    float intensity = float.Parse(parameters[2]);
                    commandList.Add(new WeatherSet(weatherType, intensity).GetCommand());

                }
                else if (command.IndexOf("land") > -1)
                {
                    commandList.Add(new Land().GetCommand());
                }
            }

            return commandList;

        }
    }

}
