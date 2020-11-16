﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Collections;
using MessagePack;
using System.Net.Sockets;
using System.Net;
using DroneBlocksAirSim.Commands;
using System.Threading;

namespace DroneBlocksAirSim
{
    public class TCP
    {
        private ArrayList commands;

        public TCP()
        {

        }

        public void Send()
        {

            var enableApiControl = new MessagePackCommand
            {
                Request = 0,
                MessageId = 0,
                Method = "enableApiControl",
                args = new ArrayList { true, "" }
            };

            var arm = new MessagePackCommand
            {
                Request = 0,
                MessageId = 1,
                Method = "armDisarm",
                args = new ArrayList { true, "" }
            };

            var disarm = new MessagePackCommand
            {
                Request = 0,
                MessageId = 1,
                Method = "armDisarm",
                args = new ArrayList { false, "" }
            };

            var flyRight = new MessagePackCommand
            {
                Request = 0,
                MessageId = 1,
                Method = "moveToPosition",
                // x y z velocity
                args = new ArrayList { 20, 20, -10, 5, 60, 0, new YawMode { is_rate = false, yaw_or_rate = 0 }, -1, 1, "" }
            };

            var flyBackward = new MessagePackCommand
            {
                Request = 0,
                MessageId = 1,
                Method = "moveToPosition",
                // x y z velocity
                args = new ArrayList { 0, 20, -10, 5, 60, 0, new YawMode { is_rate = false, yaw_or_rate = 0 }, -1, 1, "" }
            };

            var flyLeft = new MessagePackCommand
            {
                Request = 0,
                MessageId = 1,
                Method = "moveToPosition",
                // x y z velocity
                args = new ArrayList { 0, 0, -10, 5, 60, 0, new YawMode { is_rate = false, yaw_or_rate = 0 }, -1, 1, "" }
            };

            commands = new ArrayList { enableApiControl, arm, new Takeoff().getCommand(), new FlyForward(10).getCommand(), new Land().getCommand() };

            TcpClient tcpClient = new TcpClient();

            try
            {
                tcpClient.Connect("127.0.0.1", 41451);
                NetworkStream stream = tcpClient.GetStream();

                foreach (MessagePackCommand command in commands)
                {
                    Byte[] message = MessagePackSerializer.Serialize(command);
                    stream.Write(message, 0, message.Length);
                    var response = new Byte[128];
                    Int32 bytes = stream.Read(response, 0, response.Length);
                    Debug.WriteLine(command.Method + " response: " + bytes.ToString());

                    if (command.Method == "moveToPosition")
                    {
                        Debug.WriteLine("pausing");
                        Thread.Sleep(5000);
                    }
                }

                Debug.WriteLine("Mission complete");

                tcpClient.Close();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
            }

        }
    }

}