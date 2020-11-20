using System.Windows;
using System.Windows.Automation.Peers;

namespace WPF_NarratorAnnouncements
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void LiveRegionCB_Click(object sender, RoutedEventArgs e)
        {
            RaiseLiveRegionChangedEvent("Zero results found");
        }

        private void RaiseLiveRegionChangedEvent(string announcement)
        {
            // The the text on the TextBlock as the source of the announcement.
            LiveRegionTB.Text = announcement;

            // Now raise an event such that Narrator will announce the text
            // set on the announcement. 
            var peer = FrameworkElementAutomationPeer.FromElement(LiveRegionTB);
            if (peer != null)
            {
                peer.RaiseAutomationEvent(AutomationEvents.LiveRegionChanged);
            }

            // Note: That text will remain accessible to the customer using 
            // Narrator until action is explicitly taken to remove it. So if
            // it would be misleading for the customer to encounter the text
            // later, set a timer to clear the text.
        }
    }
}
