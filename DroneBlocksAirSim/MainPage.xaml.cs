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

        public MainPage()
        {
            this.InitializeComponent();
            webView.ScriptNotify += webView_ScriptNotify;
        }

        async void webView_ScriptNotify(object sender, NotifyEventArgs e)
        {
            await LaunchMission(e.Value);
        }

        private async Task LaunchMission(string commandStr)
        {

            await Task.Run(() =>
            {
                Debug.WriteLine("Raw mission string: " + commandStr);

                // Launch code is provided from webView
                MissionBuilder mb = new MissionBuilder(commandStr);
                var commandArray = mb.parseMission();

                // After mission is parsed we loop and send commands
                MissionHandler mh = new MissionHandler();
                mh.StartMissionLoop(commandArray);
            });

        }

    }
}
