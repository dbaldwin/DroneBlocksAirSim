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

            var takeoff = new MessagePackCommand
            {
                Request = 0,
                MessageId = 1,
                Method = "takeoff",
                args = new ArrayList { 20, "" }
            };

            var land = new MessagePackCommand
            {
                Request = 0,
                MessageId = 1,
                Method = "land",
                args = new ArrayList { 20, "" }
            };

            var flyForward = new MessagePackCommand
            {
                Request = 0,
                MessageId = 1,
                Method = "moveToPosition",
                // x y z velocity
                args = new ArrayList { 20, 0, -10, 5, 60, 0, new YawMode { is_rate = false, yaw_or_rate = 0 }, -1, 1, "" }
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
                Debug.WriteLine("takeoff complete: " + bytes.ToString());

                command = MessagePackSerializer.Serialize(flyForward);
                stream.Write(command, 0, command.Length);
                bytes = stream.Read(response, 0, response.Length);
                Debug.WriteLine("fly forward complete: " + bytes.ToString());

                command = MessagePackSerializer.Serialize(flyRight);
                stream.Write(command, 0, command.Length);
                bytes = stream.Read(response, 0, response.Length);
                Debug.WriteLine("fly right complete: " + bytes.ToString());

                command = MessagePackSerializer.Serialize(flyBackward);
                stream.Write(command, 0, command.Length);
                bytes = stream.Read(response, 0, response.Length);
                Debug.WriteLine("fly backward complete: " + bytes.ToString());

                command = MessagePackSerializer.Serialize(flyLeft);
                stream.Write(command, 0, command.Length);
                bytes = stream.Read(response, 0, response.Length);
                Debug.WriteLine("fly left complete: " + bytes.ToString());

                command = MessagePackSerializer.Serialize(land);
                stream.Write(command, 0, command.Length);
                bytes = stream.Read(response, 0, response.Length);
                Debug.WriteLine("land complete: " + bytes.ToString());

                /*command = MessagePackSerializer.Serialize(disarm);
                stream.Write(command, 0, command.Length);
                bytes = stream.Read(response, 0, response.Length);
                Debug.WriteLine("disarm complete: " + bytes.ToString());*/

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