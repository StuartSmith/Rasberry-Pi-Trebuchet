using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template10.Mvvm;
using Trebuchet.Interfaces;
using Trebuchet.UI.Controls.Converters;
using Windows.UI.Xaml.Media;

namespace Trebuchet.ViewModels
{
    public class MainPageFlipViewViewModel : ViewModelBase,  IMainPaigeFlipViewModel
    {
        public bool isConfigurationSetting { get; set; }   
       
        public MainPageViewModel MainPageViewModel { get; set; }
        public string Name { get; set; }
        public int PiConfigid { get; set; }
        public string PiIp { get; set; }
        public string PiName { get; set; }
        /// <summary>
        ///  Color to Use to When the light is Highlighted
        /// </summary>
        public string ColorLedLight { get; set; }
        public string ColorLedLightOff { get; set; }
        /// <summary>
        ///  Color to Set the Led Strokes
        /// </summary>
        public string ColorLedStroke { get; set; }
        /// <summary>
        /// Background color for the panel
        /// </summary>
        public string ColorPanelHighlight { get; set; }
        public string ColorLedLightLeft { get; set; }
        public string ColorLedLightRight { get; set; }
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
        public bool UserAzure { get; set; }

        public bool UseIP { get; set; }

        #region lightCommands
        DelegateCommand<MainPageFlipViewViewModel> _BothLightsOffCommand = null;
        public DelegateCommand<MainPageFlipViewViewModel> BothLightsOffCommand => _BothLightsOffCommand ?? (_BothLightsOffCommand = new DelegateCommand<MainPageFlipViewViewModel>((o) =>
        {
            //this.MainPageViewModel.SelectedFlipViewItem = o;
        }
        , (o) => true));

        DelegateCommand<MainPageFlipViewViewModel> _BothLightsOnCommand = null;
        public DelegateCommand<MainPageFlipViewViewModel> BothLightsOnCommand => _BothLightsOnCommand ?? (_BothLightsOnCommand = new DelegateCommand<MainPageFlipViewViewModel>((o) =>
        {
            //this.MainPageViewModel.SelectedFlipViewItem = o;
        }
        , (o) => true));


        DelegateCommand<MainPageFlipViewViewModel> _LeftLightOffCommand = null;
        public DelegateCommand<MainPageFlipViewViewModel> LeftLightOffCommand => _LeftLightOffCommand ?? (_LeftLightOffCommand = new DelegateCommand<MainPageFlipViewViewModel>((o) =>
        {
            //this.MainPageViewModel.SelectedFlipViewItem = o;
        }
        , (o) => true));

        DelegateCommand<MainPageFlipViewViewModel> _LeftLightOnCommand = null;
        public DelegateCommand<MainPageFlipViewViewModel> LeftLightOnCommand => _LeftLightOnCommand ?? (_LeftLightOnCommand = new DelegateCommand<MainPageFlipViewViewModel>((o) =>
        {
            //this.MainPageViewModel.SelectedFlipViewItem = o;
        }
        , (o) => true));

        DelegateCommand<MainPageFlipViewViewModel> _RightLightOffCommand = null;
        public DelegateCommand<MainPageFlipViewViewModel> RightLightOffCommand => _RightLightOffCommand ?? (_RightLightOffCommand = new DelegateCommand<MainPageFlipViewViewModel>((o) =>
        {
            //this.MainPageViewModel.SelectedFlipViewItem = o;
        }
        , (o) => true));

        DelegateCommand<MainPageFlipViewViewModel> _RightLightOnCommand = null;
        public DelegateCommand<MainPageFlipViewViewModel> RightLightOnCommand => _RightLightOffCommand ?? (_RightLightOnCommand = new DelegateCommand<MainPageFlipViewViewModel>((o) =>
        {
            //this.MainPageViewModel.SelectedFlipViewItem = o;
        }
        , (o) => true));

        DelegateCommand<MainPageFlipViewViewModel> _RefreshLightsCommand = null;
        public DelegateCommand<MainPageFlipViewViewModel> RefreshLightsCommand => _RefreshLightsCommand ?? (_RefreshLightsCommand = new DelegateCommand<MainPageFlipViewViewModel>((o) =>
        {
            //this.MainPageViewModel.SelectedFlipViewItem = o;
        }
        , (o) => true));

        #endregion

        DelegateCommand<MainPageFlipViewViewModel> _SelectCommand = null;
        public DelegateCommand<MainPageFlipViewViewModel> SelectCommand => _SelectCommand ?? (_SelectCommand = new DelegateCommand< MainPageFlipViewViewModel> ((o) =>
        {
            this.MainPageViewModel.SelectedFlipViewItem = o;
        }
        , (o) => true));


    }
}
