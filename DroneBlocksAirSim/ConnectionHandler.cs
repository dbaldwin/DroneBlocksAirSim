using DroneBlocksAirSim.Commands;
using MessagePack;
using System;
using System.Diagnostics;
using System.Threading;

namespace DroneBlocksAirSim
{
    class ConnectionHandler
    {
        private TCP client;
        private Thread connectionThread;
        private MainPage mainPage;

        public ConnectionHandler()
        {
        }

        public void BeginConnection(MainPage mainPage)
        {
            this.mainPage = mainPage;

            var ts = new ThreadStart(CheckConnection);
            connectionThread = new Thread(ts);
            connectionThread.Start();
        }

        private void CheckConnection()
        {
            client = new TCP();

            Debug.WriteLine("Thread is running");

            while (!DroneStatus.IsConnected)
            {
                Debug.WriteLine("Checking connection");

                try
                {
                    byte[] response = client.Send(new GetServerVersion().GetCommand(), 32);
                    Debug.WriteLine(MessagePackSerializer.ConvertToJson(response));
                    
                    if (response.Length > 0)
                    {
                        Debug.WriteLine("We're connected!");
                        DroneStatus.IsConnected = true;


                        mainPage.UpdateButton("Connected!");
                        
                        client.Close();
                    }

                } catch (Exception e)
                {
                    Debug.WriteLine("Error: " + e.Message);
                }

                // Check connection every 3 seconds
                Thread.Sleep(3000);
            }
        }
    }
}
