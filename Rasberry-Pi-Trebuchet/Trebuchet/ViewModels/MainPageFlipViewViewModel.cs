using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template10.Mvvm;
using Trebuchet.Interfaces;
using Trebuchet.Converters;
using Windows.UI.Xaml.Media;

namespace Trebuchet.ViewModels
{
    public class MainPageFlipViewViewModel : ViewModelBase, IPiSettingsAndConfiguration
    {
        public bool isConfigurationSetting { get; set; }     
       
        public MainPageViewModel MainPageViewModel { get; set; }
        public string Name { get; set; }
        public int PiConfigid { get; set; }
        public string PiIP { get; set; }
        public string PiName { get; set; }
        public string ColorLedLight { get; set; }
        public string ColorLedStroke { get; set; }
        public string ColorPanelHighlight { get; set; }






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


        DelegateCommand<MainPageFlipViewViewModel> _SelectCommand = null;
        public DelegateCommand<MainPageFlipViewViewModel> SelectCommand => _SelectCommand ?? (_SelectCommand = new DelegateCommand< MainPageFlipViewViewModel> ((o) =>
        {
            this.MainPageViewModel.SelectedFlipViewItem = o;
        }
        , (o) => true));
    }
}
