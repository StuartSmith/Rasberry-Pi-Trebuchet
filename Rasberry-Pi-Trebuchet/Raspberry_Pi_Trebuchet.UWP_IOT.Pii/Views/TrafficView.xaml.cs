using Raspberry_Pi_Trebuchet.RestUp.Azure.Services;
using Raspberry_Pi_Trebuchet.UWP_IOT.Pii.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Raspberry_Pi_Trebuchet.UWP_IOT.Pii.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TrafficView : Page
    {
        private VM_TrafficView vm_TrafficView = new VM_TrafficView();

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Await.Warning", "CS4014:Await.Warning")]
        public TrafficView()
        {
            this.InitializeComponent();
            this.DataContext = vm_TrafficView;

            AzureMsgListener.Instance.StartAzureMsgListener();

            vm_TrafficView.InitializeWebServer();

        }
    }
}
