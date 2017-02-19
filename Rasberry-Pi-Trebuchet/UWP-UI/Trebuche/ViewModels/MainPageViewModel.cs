using Template10.Mvvm;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading.Tasks;
using Template10.Services.NavigationService;
using Windows.UI.Xaml.Navigation;

namespace Trebuchet.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        public MainPageViewModel()
        {
            if (Windows.ApplicationModel.DesignMode.DesignModeEnabled)
            {
                RemoteAddress = "stsp://localhost";
            }
        }

        #region videocalls
        string _RemoteAddress = null;
        public string RemoteAddress { get { return _RemoteAddress; } set { Set(ref _RemoteAddress, value); } }


        Uri _VideoSource = null;
        public Uri VideoSource { get { return _VideoSource; } set { Set(ref _VideoSource, value); } }

        #endregion
        

        System.Collections.ObjectModel.ObservableCollection<MainPageFlipViewViewModel> _FlipViewViewModels = new System.Collections.ObjectModel.ObservableCollection<MainPageFlipViewViewModel>();
        public System.Collections.ObjectModel.ObservableCollection<MainPageFlipViewViewModel> FlipViewViewModels { get { return _FlipViewViewModels; } }

       

        MainPageFlipViewViewModel _SelectedFlipViewItem = default(MainPageFlipViewViewModel);
        public MainPageFlipViewViewModel SelectedFlipViewItem
        {
            get { return _SelectedFlipViewItem; }
            set
            {
                // base.SetProperty(ref _SelectedColor, value);
                foreach (var item in FlipViewViewModels.Where(x => x.Selected))
                    item.Selected= false;
                if (value != null)
                {
                    value.Selected = true;
                    if (_SelectedFlipViewItem != value)
                    {
                        _SelectedFlipViewItem = value;
                        base.RaisePropertyChanged();
                    }


                }
            }
        }

       


        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> suspensionState)
        {
            if (!(this._FlipViewViewModels.Any<MainPageFlipViewViewModel>()))
            {
                FlipViewViewModels.Add(new MainPageFlipViewViewModel()
                {
                    ColorPanelHighlight = "#FFFFCCCC",
                    isConfigurationSetting = false,
                    Name = "Red Team",
                    MainPageViewModel = this,
                    PiConfigid = 1,
                    PiIp = "100.100.100.100",
                    PiName = "WfRedTeam",
                    Selected = true,
                    ColorLedLight = "#FFFF0000",

                    ColorLedLightOff = "#FFFFFFFF",
                    ColorLedLightLeft = "#FFFFFFFF",
                    ColorLedLightRight = "#FFFFFFFF",
                    ColorLedStrokeLeft = "#FF000000",
                    ColorLedStrokeRight = "#FF000000",

                }
                );

                FlipViewViewModels.Add(new MainPageFlipViewViewModel()
                {
                    ColorPanelHighlight = "#FFCDEACDC",
                    isConfigurationSetting = false,
                    Name = "Green Team",
                    MainPageViewModel = this,
                    PiConfigid = 1,
                    PiIp = "100.100.100.101",
                    PiName = "WfGreenTeam",
                    Selected = false,
                    ColorLedLight = "#FF00FF00",
                    ColorLedLightOff = "#FFFFFFFF",
                    ColorLedLightLeft = "#FFFFFFFF",
                    ColorLedLightRight = "#FFFFFFFF",
                    ColorLedStrokeLeft = "#FF000000",
                    ColorLedStrokeRight = "#FF000000",

                }
               );


                FlipViewViewModels.Add(new MainPageFlipViewViewModel()
                {
                    ColorPanelHighlight = "#FFD9EDF7",
                    isConfigurationSetting = false,
                    Name = "Blue Team",
                    MainPageViewModel = this,
                    PiConfigid = 1,
                    PiIp = "100.100.100.101",
                    PiName = "WfBlueTeam",
                    Selected = false,
                    ColorLedLight = "#FF0000FF",
                    ColorLedLightOff = "#FFFFFFFF",
                    ColorLedLightLeft = "#FFFFFFFF",
                    ColorLedLightRight = "#FFFFFFFF",
                    ColorLedStrokeLeft = "#FF000000",
                    ColorLedStrokeRight = "#FF000000",

                }
               );

                FlipViewViewModels.Add(new MainPageFlipViewViewModel()
                {
                    ColorPanelHighlight = "#FF787878",
                    isConfigurationSetting = true,
                    Name = "Settings",
                    MainPageViewModel = this,
                    PiConfigid = 1,
                    PiIp = "",
                    PiName = "",
                    Selected = false,
                    ColorLedLight = "#FF787878",
                    ColorLedLightOff = "#FFFFFFFF",
                    ColorLedLightLeft = "#FFFFFFFF",
                    ColorLedLightRight = "#FFFFFFFF",
                    ColorLedStrokeLeft = "#FF000000",
                    ColorLedStrokeRight = "#FF000000",

                }
               );



            }

            if (suspensionState.Any())
            {
                //Value = suspensionState[nameof(Value)]?.ToString();
            }
            await Task.CompletedTask;
        }

        public async Task<bool> StartCall()
        {   
            VideoSource = new Uri("stsp://" + RemoteAddress);
            return true;
        }




        #region MainPageNav
        public override async Task OnNavigatedFromAsync(IDictionary<string, object> suspensionState, bool suspending)
        {
            if (suspending)
            {
                //suspensionState[nameof(Value)] = Value;
            }
            await Task.CompletedTask;
        }

        public override async Task OnNavigatingFromAsync(NavigatingEventArgs args)
        {
            args.Cancel = false;
            await Task.CompletedTask;
        }

        public void GotoDetailsPage() =>
            NavigationService.Navigate(typeof(Views.DetailPage), null);

        public void GotoSettings() =>
            NavigationService.Navigate(typeof(Views.SettingsPage), 0);

        public void GotoPrivacy() =>
            NavigationService.Navigate(typeof(Views.SettingsPage), 1);

        public void GotoAbout() =>
            NavigationService.Navigate(typeof(Views.SettingsPage), 2);
        #endregion

    }
}

