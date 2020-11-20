using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Controls;

namespace WPF_NarratorAnnouncements
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // Collapse the live TextBlock when it has no text set on it.
            DependencyPropertyDescriptor dp = DependencyPropertyDescriptor.FromProperty(
                TextBlock.TextProperty, typeof(TextBlock));

            dp.AddValueChanged(LiveRegionTB, (object a, EventArgs b) =>
            {
                // If the TextBlock has no text set, set its Visibility to Collapsed. 
                // By doing so, in .NET Framework 4.8, it will no be exposed through
                // UIA at all.
                LiveRegionTB.Visibility = (LiveRegionTB.Text.Trim() == "" ?
                    Visibility.Collapsed : Visibility.Visible);
            });
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
