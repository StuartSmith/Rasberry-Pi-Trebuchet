using Raspberry_Pi_Trebuchet.UWP_UI.Desktop.Requestors.Lights.Manager;
using System.Linq;
using Template10.Mvvm;
using Trebuchet.Interfaces;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;


namespace Trebuchet.ViewModels
{
    public class MainPageFlipViewViewModel : ViewModelBase,  IMainPaigeFlipViewModel
    {
        #region properties
        public string ColorLedLight { get; set; }
        public string ColorLedLightOff { get; set; }
        /// <summary>
        ///  Color to Set the Led Strokes
        /// </summary>
        public string ColorLedStroke { get; set; }
        public bool isConfigurationSetting { get; set; }  
        public MainPageViewModel MainPageViewModel { get; set; }
        public string Name { get; set; }
        public int PiConfigid { get; set; }
        public string PiIp { get; set; }
        public string PiName { get; set; } 
        /// <summary>
        /// Background color for the panel
        /// </summary>
        public string ColorPanelHighlight { get; set; }

        public string _ColorLedLightLeft;
        public string ColorLedLightLeft
        {
            get
            {
                return _ColorLedLightLeft;
            }
            set
            {
                if (_ColorLedLightLeft != value)
                {
                    _ColorLedLightLeft = value;
                    base.RaisePropertyChanged();
                }
            }       
        }

        public string _ColorLedLightRight;
        public string ColorLedLightRight
        {
            get
            {
                return _ColorLedLightRight;
            }
            set
            {
                if (_ColorLedLightRight != value)
                {
                    _ColorLedLightRight = value;
                    base.RaisePropertyChanged();
                }
            }
        }
        public string ColorLedStrokeLeft { get; set; }
        public string ColorLedStrokeRight { get; set; }
        bool _Selected = default(bool);
        public bool Selected
        {
            get
            {
                return _Selected;
            }
            set
            {
                if (_Selected != value)
                {
                    _Selected = value;
                    base.RaisePropertyChanged();
                }

            }
        }
        public bool SendToast { get; set; }
        private bool _UseAzure;
        public bool UseAzure {
            get
            {
                return _UseAzure;
            }
            set
            {
                if (_UseAzure != value)
                {
                    _UseAzure = value;
                    base.RaisePropertyChanged();
                }
            }
        }

        #endregion

        #region lightCommands
        DelegateCommand<MainPageFlipViewViewModel> _BothLightsOffCommand = null;
        public DelegateCommand<MainPageFlipViewViewModel> BothLightsOffCommand => _BothLightsOffCommand ?? (_BothLightsOffCommand = new DelegateCommand<MainPageFlipViewViewModel>(async (o) => 
        {
            var lightManager = new LightsManager(this);

            await lightManager.TurnBothLightsOff();

        }
        , (o) => true));      
        

        DelegateCommand<MainPageFlipViewViewModel> _BothLightsOnCommand = null;
        public DelegateCommand<MainPageFlipViewViewModel> BothLightsOnCommand => _BothLightsOnCommand ?? (_BothLightsOnCommand = new DelegateCommand<MainPageFlipViewViewModel>(async (o) =>
        {
            var lightManager = new LightsManager(this);                   
            await lightManager.TurnBothLightsOn();
        }
        , (o) => true));


        DelegateCommand<MainPageFlipViewViewModel> _LeftLightOffCommand = null;
        public DelegateCommand<MainPageFlipViewViewModel> LeftLightOffCommand => _LeftLightOffCommand ?? (_LeftLightOffCommand = new DelegateCommand<MainPageFlipViewViewModel>( async (o) =>
        {
            var lightManager = new LightsManager(this);
            await lightManager.TurnLeftLightOff();  
        }
        , (o) => true));

        DelegateCommand<MainPageFlipViewViewModel> _LeftLightOnCommand = null;
        public DelegateCommand<MainPageFlipViewViewModel> LeftLightOnCommand => _LeftLightOnCommand ?? (_LeftLightOnCommand = new DelegateCommand<MainPageFlipViewViewModel>(async (o) =>
        {
            var lightManager = new LightsManager(this);
            await lightManager.TurnLeftLightOn();            
        }
        , (o) => true));

        DelegateCommand<MainPageFlipViewViewModel> _RightLightOffCommand = null;
        public DelegateCommand<MainPageFlipViewViewModel> RightLightOffCommand => _RightLightOffCommand ?? (_RightLightOffCommand = new DelegateCommand<MainPageFlipViewViewModel>(async (o) =>
       {
           var lightManager = new LightsManager(this);
           await lightManager.TurnRightLightOff();
       }
        , (o) => true));

        DelegateCommand<MainPageFlipViewViewModel> _RightLightOnCommand = null;
        public DelegateCommand<MainPageFlipViewViewModel> RightLightOnCommand => _RightLightOnCommand ?? (_RightLightOnCommand = new DelegateCommand<MainPageFlipViewViewModel>(async (o) =>
        {
            var lightManager = new LightsManager(this);
            await lightManager.TurnRightLightOn();
        }
        , (o) => true));

        DelegateCommand<MainPageFlipViewViewModel> _RefreshLightsCommand = null;
        public DelegateCommand<MainPageFlipViewViewModel> RefreshLightsCommand => _RefreshLightsCommand ?? (_RefreshLightsCommand = new DelegateCommand<MainPageFlipViewViewModel>(async (o) =>
        {
            var lightManager = new LightsManager(this);
            await lightManager.GetLightStatuses();

        }
        , (o) => true));

        #endregion

        #region Commands
        DelegateCommand<MainPageFlipViewViewModel> _SelectCommand = null;
        public DelegateCommand<MainPageFlipViewViewModel> SelectCommand => _SelectCommand ?? (_SelectCommand = new DelegateCommand< MainPageFlipViewViewModel> ((o) =>
        {
            this.MainPageViewModel.SelectedFlipViewItem = o;
        }
        , (o) => true));

        DelegateCommand<MainPageFlipViewViewModel> _CommandToggleUseAzure = null;

        public DelegateCommand<MainPageFlipViewViewModel> CommandToggleUseAzure => _CommandToggleUseAzure ?? (_CommandToggleUseAzure = new DelegateCommand<MainPageFlipViewViewModel>((o) =>
        {
               //Update all the User Azure values
               MainPageViewModel.FlipViewViewModels.Where(x=> x.isConfigurationSetting == false)
                                                         .Select(x => { x.UseAzure = this.UseAzure; return x; })
                                                         .ToList();
        }
       , (o) => true));

        #endregion


        public void ToggleUseAzure(object sender, RoutedEventArgs e)
        {
            ToggleSwitch ts = (ToggleSwitch)sender;

            MainPageViewModel.FlipViewViewModels.Where(x => x.isConfigurationSetting == ts.IsOn)
                                                         .Select(x => { x.UseAzure = this.UseAzure; return x; })
                                                         .ToList();

        }

    }
}
