using System.Diagnostics;
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

            Debug.WriteLine("Raw mission string: " + e.Value);

            // Temporary because of cache
            if (e.Value.IndexOf("Please") > -1) return;

            // Launch code is provided from webView
            MissionBuilder mb = new MissionBuilder(e.Value);
            var commandArray = mb.parseMission();

            MissionHandler mh = new MissionHandler();
            mh.StartMissionLoop(commandArray);

        }

    }
}
