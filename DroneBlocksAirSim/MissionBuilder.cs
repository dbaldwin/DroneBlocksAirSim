﻿using DroneBlocksAirSim.Commands;
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
                else if (command.IndexOf("coordinate_system") > -1)
                {

                }
                // We can handle these in the same statement because the blocks will send a negative value for backward flight
                else if (command.IndexOf("fly_x") > -1)
                {
                    float xvelocity = float.Parse(parameters[1]);
                    float duration = float.Parse(parameters[2]);
                    Debug.WriteLine("fly by x velocity {0} for {1} s", xvelocity, duration);
                    commandList.Add(new MoveByVelocity(xvelocity, 0, 0, duration).GetCommand());
                }
                else if (command.IndexOf("fly_y") > -1)
                {
                    float yvelocity = float.Parse(parameters[1]);
                    float duration = float.Parse(parameters[2]);
                    Debug.WriteLine("fly by y velocity {0} for {1} s", yvelocity, duration);
                    commandList.Add(new MoveByVelocity(0, yvelocity, 0, duration).GetCommand());
                }
                else if (command.IndexOf("fly_z") > -1)
                {
                    float zvelocity = float.Parse(parameters[1]);
                    float duration = float.Parse(parameters[2]);
                    Debug.WriteLine("fly by z velocity {0} for {1} s", zvelocity, duration);
                    commandList.Add(new MoveByVelocity(0, 0, zvelocity, duration).GetCommand());
                }
                else if (command.IndexOf("fly_to_location") > -1)
                {
                    float xposition = float.Parse(parameters[1]);
                    float yposition = float.Parse(parameters[2]);
                    float zposition = float.Parse(parameters[3]);
                    Debug.WriteLine("flying to location {0}, {1}, {2}", xposition, yposition, zposition);
                    commandList.Add(new MoveToPosition(xposition, yposition, zposition).GetCommand());
                }
                else if (command.IndexOf("fly_rpy") > -1)
                {
                    float roll = float.Parse(parameters[1]);
                    float pitch = float.Parse(parameters[2]);
                    float yaw = float.Parse(parameters[3]);
                    float duration = float.Parse(parameters[4]);
                    Debug.WriteLine("flying with roll {0}, pitch {1}, yaw {2}, for {3} seconds", roll, pitch, yaw, duration);
                    commandList.Add(new MoveByRollPitchYaw(roll, pitch, yaw, duration).GetCommand());
                }
                else if (command.IndexOf("hover") > -1)
                {
                    float delay = float.Parse(parameters[1]);
                    commandList.Add(new Hover(delay).GetCommand());
                }
                else if (command.IndexOf("rotate_to_yaw") > -1)
                {
                    float yaw = float.Parse(parameters[1]);
                    commandList.Add(new RotateToYaw(yaw).GetCommand());
                }
                else if (command.IndexOf("rotate_yaw_rate") > -1)
                {
                    float yawRate = float.Parse(parameters[1]);
                    float duration = float.Parse(parameters[2]);
                    commandList.Add(new RotateByYawRate(yawRate, duration).GetCommand());
                }
                else if (command.IndexOf("wind") > -1)
                {
                    int x_vel = int.Parse(parameters[1]);
                    int y_vel = int.Parse(parameters[2]);
                    int z_vel = int.Parse(parameters[3]);
                    commandList.Add(new Wind(x_vel, y_vel, z_vel).GetCommand());
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
                    // Before landing we're going to need to introduce a delay
                    // It seems like AirSim ignores the command if the drone is 
                    // trying to stabilize itself
                    commandList.Add(new Hover(3).GetCommand());
                    commandList.Add(new Land().GetCommand());
                }
            }

            return commandList;

        }
    }

}
