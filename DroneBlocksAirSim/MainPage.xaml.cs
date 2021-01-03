using System.Diagnostics;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace DroneBlocksAirSim
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        private MissionStatus missionStatus;

        public MainPage()
        {
            this.InitializeComponent();
            webView.ScriptNotify += webView_ScriptNotify;
            
            // TODO: Implement this
            missionStatus = new MissionStatus();

            // Trying to clear the fucking cache. It's killing me.
            WebView.ClearTemporaryWebDataAsync();
        }

        async void webView_ScriptNotify(object sender, NotifyEventArgs e)
        {
            await LaunchMission(e.Value);
        }

        private async Task LaunchMission(string commandStr)
        {

            /*if (!DroneStatus.IsConnected)
            {
                Debug.WriteLine("Drone is not connected!");
                return;
            }*/

            await Task.Run(() =>
            {

                /*new DroneStatus().GetState();
                return;*/

                Debug.WriteLine("Raw mission string: " + commandStr);

                // Launch code is provided from webView
                MissionBuilder mb = new MissionBuilder(commandStr);
                var commandArray = mb.parseMission();

                // After mission is parsed we loop and send commands
                MissionHandler mh = new MissionHandler(missionStatus);
                mh.StartMissionLoop(commandArray);
            });

        }

        private void Connect(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Clicked!");
            //var connect = new ConnectionHandler();
            //connect.BeginConnection(this);
        }

        public void UpdateButton(string label)
        {
            ConnectButton.Content = label;
        }
    }
}
