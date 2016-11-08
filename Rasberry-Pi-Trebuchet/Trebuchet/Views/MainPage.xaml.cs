using System;
using Trebuchet.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using System.Collections.ObjectModel;
using Trebuchet.Services.CaptureDeviceService;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Input;
using System.Threading;
using System.Threading.Tasks;
using Windows.Media.MediaProperties;
using Windows.Media.Capture;
using Microsoft.Samples.SimpleCommunication;
using Windows.UI.Core;

namespace Trebuchet.Views
{
    public sealed partial class MainPage : Page
    {

        CaptureDevice device = null;
        bool? roleIsActive = null;
        int isTerminator = 0;
        bool activated = false;

        private static Windows.Media.MediaExtensionManager mediaExtensionMgr;


        public MainPage()
        {
            InitializeComponent();
            NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Enabled;
            EnsureMediaExtensionManager();
        }

        public void EnsureMediaExtensionManager()
        {
            if (mediaExtensionMgr == null)
            {
                mediaExtensionMgr = new Windows.Media.MediaExtensionManager();
                mediaExtensionMgr.RegisterSchemeHandler("Microsoft.Samples.SimpleCommunication.StspSchemeHandler", "stsp:");
            }
        }

        #region Thumb divider
        void OnThumbDragStarted(object sender, DragStartedEventArgs args)
        {
            OnDragStart((Thumb)sender);
        }

        void OnThumbDragDelta(object sender, DragDeltaEventArgs args)
        {
            OnDragDelta((Thumb)sender, args.VerticalChange);
        }

        void OnThumbGotFocus(object sender, RoutedEventArgs args)
        {
            OnDragStart((Thumb)sender);
        }



        void OnDragStart(Thumb thumb)
        {
            Grid grid = (Grid)thumb.Parent;
            RowDefinition TopRowDef = grid.RowDefinitions[0];
            RowDefinition BottomRowDef = grid.RowDefinitions[2];

            TopRowDef.Height =
                new GridLength(TopRowDef.ActualHeight, GridUnitType.Star);
            BottomRowDef.Height =
                 new GridLength(BottomRowDef.ActualHeight, GridUnitType.Star);
        }

        void OnDragDelta(Thumb thumb, double change)
        {
            Grid grid = (Grid)thumb.Parent;
            RowDefinition TopRowDef = grid.RowDefinitions[0];
            RowDefinition BottomRowDef = grid.RowDefinitions[2];

            if (TopRowDef.Height.Value + change < 0)
                TopRowDef.Height = new GridLength(0, GridUnitType.Star);
            else
                TopRowDef.Height =
                    new GridLength(TopRowDef.Height.Value + change, GridUnitType.Star);

            if (BottomRowDef.Height.Value - change < 0)
                BottomRowDef.Height = new GridLength(0, GridUnitType.Star);
            else
                BottomRowDef.Height =
                new GridLength(BottomRowDef.Height.Value - change, GridUnitType.Star);

            //Debug.WriteLine(To);
        }

        void OnThumbPointerEntered(object sender, PointerRoutedEventArgs args)
        {
            //Window.Current.CoreWindow.PointerCursor =
            //    new CoreCursor(CoreCursorType.SizeWestEast, 0);
        }

        void OnThumbPointerExited(object sender, PointerRoutedEventArgs args)
        {
            ///Window.Current.CoreWindow.PointerCursor =
            //    new CoreCursor(CoreCursorType.SizeWestEast, 0);
        }

        #endregion Thumb divider

        private void CallButton_Click(object sender, RoutedEventArgs e)
        {

            Button b = sender as Button;
            var address = HostNameTextbox.Text;

            if (b != null && !String.IsNullOrEmpty(address))
            {
                roleIsActive = true;
                RemoteVideo.Source = new Uri("stsp://" + address);
                HostNameTextbox.IsEnabled = CallButton.IsEnabled = false;
                // rootPage.NotifyUser("Initiating connection.. Please wait.", NotifyType.StatusMessage);
            }

            //RemoteVideo.Play();
        }

        private async void EndCallButton_Click(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;
            if (b != null && Interlocked.CompareExchange(ref isTerminator, 1, 0) == 0)
            {
                await EndCallAsync();
            }
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {

            var cameraFound = await CaptureDevice.CheckForRecordingDeviceAsync();

            if (cameraFound)
            {
                device = new CaptureDevice();
                await InitializeAsync();
                device.IncomingConnectionArrived += Device_IncomingConnectionArrived;
                device.CaptureFailed += Device_CaptureFailed;
                RemoteVideo.MediaFailed += RemoteVideo_MediaFailed;
            }
            else
            {
                // rootPage.NotifyUser("A machine with a camera and a microphone is required to run this sample.", NotifyType.ErrorMessage);
            }

            base.OnNavigatedTo(e);
        }

        private async void RemoteVideo_MediaFailed(object sender, ExceptionRoutedEventArgs e)
        {

            if (Interlocked.CompareExchange(ref isTerminator, 1, 0) == 0)
            {
                await EndCallAsync();
            }
        }

        private async void Device_CaptureFailed(object sender, MediaCaptureFailedEventArgs e)
        {
            if (Interlocked.CompareExchange(ref isTerminator, 1, 0) == 0)
            {
                await EndCallAsync();
            }
        }

        private async void Device_IncomingConnectionArrived(object sender, IncomingConnectionEventArgs e)
        {
            e.Accept();

            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, (() =>
            {
                activated = true;
                var remoteAddress = e.RemoteUrl;
                EndCallButton.IsEnabled = true;
                Interlocked.Exchange(ref isTerminator, 0);

                if (!((bool)roleIsActive))
                {
                    // Passive client
                    RemoteVideo.Source = new Uri(remoteAddress);
                    HostNameTextbox.IsEnabled = CallButton.IsEnabled = false;
                }

                remoteAddress = remoteAddress.Replace("stsp://", "");
                // rootPage.NotifyUser("Connected. Remote machine address: " + remoteAddress, NotifyType.StatusMessage);
            }));
        }

        private async Task InitializeAsync(CancellationToken cancel = default(CancellationToken))
        {
            // rootPage.NotifyUser("Initializing..", NotifyType.StatusMessage);

            try
            {
                await device.InitializeAsync();
                await StartRecordToCustomSink();

                HostNameTextbox.IsEnabled = CallButton.IsEnabled = true;
                EndCallButton.IsEnabled = false;
                RemoteVideo.Source = null;

                // Each client starts out as passive
                roleIsActive = false;
                Interlocked.Exchange(ref isTerminator, 0);

                //rootPage.NotifyUser("Tap 'CallButton.' button to start CallButton.", NotifyType.StatusMessage);
            }
            catch (Exception)
            {
                /// rootPage.NotifyUser("Initialization error. Restart the sample to try again.", NotifyType.ErrorMessage);
            }

        }

        private async Task StartRecordToCustomSink()
        {
            MediaEncodingProfile mep = MediaEncodingProfile.CreateMp4(VideoEncodingQuality.Qvga);


            mep.Video.FrameRate.Numerator = 15;
            mep.Video.FrameRate.Denominator = 1;
            mep.Container = null;

            await device.StartRecordingAsync(mep);
        }

        private async Task EndCallAsync()
        {
            await device.CleanUpAsync();

            // end the CallButton. session
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, (() =>
            {
                RemoteVideo.Stop();
                RemoteVideo.Source = null;
            }));

            // Start waiting for a new CallButton.
            await InitializeAsync();
        }
    }
}
