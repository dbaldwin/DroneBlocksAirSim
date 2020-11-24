using System;
using System.Diagnostics;
using System.Collections;
using MessagePack;
using System.Net.Sockets;
using System.Text;

namespace DroneBlocksAirSim
{
    public class TCP
    {
        private ArrayList commands;
        private TcpClient client;
        private NetworkStream stream;

        public TCP()
        {
            try
            {
                client = new TcpClient();
                client.Connect("127.0.0.1", 41451);
                stream = client.GetStream();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
            }
            
        }

        public byte[] Send(MessagePackCommand command, int responseLength)
        {

            if (stream == null)
            {
                return new Byte[0];
                
            }

            Byte[] message = MessagePackSerializer.Serialize(command);
            stream.Write(message, 0, message.Length);
            var response = new Byte[responseLength];
            Int32 bytes = stream.Read(response, 0, response.Length);
            return response;
            //Debug.WriteLine(command.Method + " response: " + Encoding.UTF8.GetString(response, 0, response.Length));
        }

        public void Close()
        {
            stream.Close();
            client.Close();
        }

    }

}