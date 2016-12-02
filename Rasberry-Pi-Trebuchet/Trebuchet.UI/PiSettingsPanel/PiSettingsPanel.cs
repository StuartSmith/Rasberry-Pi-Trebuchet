using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Windows.Input;
using Trebuchet.UI.Controls.Converters;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;

// The Templated Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234235

namespace Trebuchet.UI.Controls
{
    public sealed class PiSettingsPanel : Control
    {
        public PiSettingsPanel()
        {
            this.DefaultStyleKey = typeof(PiSettingsPanel);
        }

        #region BrushPanel
        public Brush BrushPanel
        {
            get { return (Brush)GetValue(BrushPanelProperty); }
            set { SetValue(BrushPanelProperty, value); }
        }
        // Using a DependencyProperty as the backing store for BrushPanel.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BrushPanelProperty =
            DependencyProperty.Register(nameof(BrushPanel), typeof(Brush), typeof(PiSettingsPanel), new PropertyMetadata(new SolidColorBrush(Colors.LightGray)));
        #endregion

        #region UseAzure

        public bool UseAzure
        {
            get { return (bool)GetValue(UseAzureProperty); }
            set { SetValue(UseAzureProperty, value); }
        }

        // Using a DependencyProperty as the backing store for UseAzure.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UseAzureProperty =
            DependencyProperty.Register(nameof(UseAzure), typeof(bool), typeof(PiSettingsPanel), new PropertyMetadata(false));

        public ICommand CommandUseAzure
        {
            get { return (ICommand)GetValue(CommandUseAzureProperty); }
            set { SetValue(CommandUseAzureProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CommandUseAzure.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CommandUseAzureProperty =
            DependencyProperty.Register(nameof(CommandUseAzure), typeof(ICommand), typeof(PiSettingsPanel), new PropertyMetadata(null));

        #endregion



    }
}
