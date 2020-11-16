using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Collections;
using MessagePack;
using System.Net.Sockets;
using System.Net;

namespace DroneBlocksAirSim
{
    public class TCP
    {

        public TCP()
        {

        }

        public void Send()
        {

            var enableApiControl = new Command
            {
                Request = 0,
                MessageId = 0,
                Method = "enableApiControl",
                args = new ArrayList { true, "" }
            };

            var arm = new Command
            {
                Request = 0,
                MessageId = 1,
                Method = "armDisarm",
                args = new ArrayList { true, "" }
            };

            var takeoff = new Command
            {
                Request = 0,
                MessageId = 1,
                Method = "takeoff",
                args = new ArrayList { 20, "" }
            };

            var land = new Command
            {
                Request = 0,
                MessageId = 1,
                Method = "land",
                args = new ArrayList { 20, "" }
            };

            TcpClient tcpClient = new TcpClient();

            try
            {
                tcpClient.Connect("127.0.0.1", 41451);

                Byte[] command = MessagePackSerializer.Serialize(enableApiControl);
                NetworkStream stream = tcpClient.GetStream();
                stream.Write(command, 0, command.Length);
                var response = new Byte[128];
                Int32 bytes = stream.Read(response, 0, response.Length);

                Debug.WriteLine("enableApiControl: " + bytes.ToString());

                command = MessagePackSerializer.Serialize(arm);
                stream.Write(command, 0, command.Length);
                response = new Byte[128];
                bytes = stream.Read(response, 0, response.Length);

                Debug.WriteLine("armDisarm: " + bytes.ToString());

                command = MessagePackSerializer.Serialize(takeoff);
                stream.Write(command, 0, command.Length);
                response = new Byte[128];
                bytes = stream.Read(response, 0, response.Length);

                Debug.WriteLine("takeoff: " + bytes.ToString());

                Debug.WriteLine("Mission Complete");


                tcpClient.Close();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
            }

        }
    }

}