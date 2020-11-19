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
        private TCP client;

        public MainPage()
        {
            this.InitializeComponent();

            TestButton.Click += TestButton_Click;

            client = new TCP();


            webView.ScriptNotify += webView_ScriptNotify;

        }

        async void webView_ScriptNotify(object sender, NotifyEventArgs e)
        {
            client.Send();
        }

        private void TestButton_Click(object sender, RoutedEventArgs e)
        {
            client.Send();
        }

    }
}
