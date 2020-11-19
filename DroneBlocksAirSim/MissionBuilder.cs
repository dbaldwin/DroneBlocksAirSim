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
            commandList.Add(new EnableApiControl().GetCommand());

            foreach (var commandString in missionString.Split("|"))
            {
                string[] parameters = commandString.Split(",");

                string command = parameters[0];

                if (command.IndexOf("takeoff") > -1)
                {
                    commandList.Add(new Takeoff().GetCommand());
                }
                else if (command.IndexOf("fly_forward") > -1)
                {
                    int distance = int.Parse(parameters[1]);
                    Debug.WriteLine("giong to fly forward: " + distance);
                    //commandList.Add(new FlyForward(distance).GetCommand());

                    // Testing move by velocity
                    commandList.Add(new MoveByVelocity(distance, 0, 0, 30).GetCommand());
                }
                else if (command.IndexOf("fly_up") > -1)
                {
                    int distance = int.Parse(parameters[1]);

                    // For now let's do the negative conversion for them
                    distance *= -1;

                    Debug.WriteLine("giong to fly up: " + distance);

                    // Testing move by velocity
                    commandList.Add(new MoveByVelocity(0, 0, distance, 30).GetCommand());
                }
                else if (command.IndexOf("fly_backward") > -1)
                {
                    int distance = int.Parse(parameters[1]);
                    commandList.Add(new FlyBackward(distance));
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
