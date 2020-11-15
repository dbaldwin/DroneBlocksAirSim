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
        private bool isDroneArmed = false;

        public TCP()
        {
            Debug.WriteLine("The UDP constructor has been called");
        }

        public void Send()
        {

            var obj = new Sample1
            {
                Request = 0,
                MessageId = 0,
                Command = "enableApiControl",
                args = new ArrayList { true, "" }
            };

            var obj2 = new Sample1
            {
                Request = 0,
                MessageId = 1,
                Command = "armDisarm",
                args = new ArrayList { false, "" }
            };

            if (!this.isDroneArmed)
            {
                obj2.args = new ArrayList { true, "" };
                Debug.WriteLine(this.isDroneArmed);
            }

            this.isDroneArmed = !this.isDroneArmed;

            //byte[] bytes = Encoding.ASCII.GetBytes(MessagePackSerializer.SerializeToJson(new Sample1 { Request = 0, MessageId = 1, Command = "enableAPIControl", EnableControl = true }));

            byte[] command = MessagePackSerializer.Serialize(obj);

            byte[] command2 = MessagePackSerializer.Serialize(obj2);

            TcpClient tcpClient = new TcpClient();
            try
            {
                tcpClient.Connect("127.0.0.1", 41451);

                // Sends a message to the host to which you have connected.
                // Byte[] sendBytes = Encoding.ASCII.GetBytes("Is anybody there?");

                NetworkStream stream = tcpClient.GetStream();
                stream.Write(command, 0, command.Length);
                stream.Write(command2, 0, command2.Length);

                //byte[] receive = tcpClient.

                // Sends a message to a different host using optional hostname and port parameters.
                //UdpClient udpClientB = new UdpClient();
                //udpClientB.Send(sendBytes, sendBytes.Length, "AlternateHostMachineName", 11000);

                //IPEndPoint object will allow us to read datagrams sent from any source.
                IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);

                // Blocks until a message returns on this socket from a remote host.
                //Byte[] receiveBytes = udpClient.Receive(ref RemoteIpEndPoint);
                //string returnData = Encoding.ASCII.GetString(receiveBytes);

                // Uses the IPEndPoint object to determine which of these two hosts responded.
                //Debug.WriteLine("This is the message you received " + returnData.ToString());
                Debug.WriteLine("This message was sent from " +
                                            RemoteIpEndPoint.Address.ToString() +
                                            " on their port number " +
                                            RemoteIpEndPoint.Port.ToString());

                tcpClient.Close();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
            }

        }
    }

    [MessagePackObject]
    public class Sample1
    {
        [Key(0)]
        public int Request { get; set; } // Should always be 0
        [Key(1)]
        public int MessageId { get; set; }
        [Key(2)]
        public string Command { get; set; }
        [Key(3)]
        public ArrayList args { get; set; }


    }
}