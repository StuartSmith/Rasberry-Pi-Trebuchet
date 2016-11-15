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

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Trebuchet.Controls
{
    public sealed partial class LedLight : UserControl
    {
        public LedLight()
        {
            this.InitializeComponent();
            LayoutRoot.DataContext = this;
        }

        public static readonly DependencyProperty LightBrushProperty = DependencyProperty.Register("LightBrush", typeof(Brush), typeof(LedLight), new PropertyMetadata(null));
        public Brush LightBrush
        {
            get { return (Brush)GetValue(LightBrushProperty); }
            set { SetValue(LightBrushProperty, value); }
        }

        public static readonly DependencyProperty LightStrokeProperty = DependencyProperty.Register("LightStroke", typeof(Brush), typeof(LedLight),  new PropertyMetadata(null));
        public Brush LightStroke
        {
            get { return (Brush)GetValue(LightStrokeProperty); }
            set { SetValue(LightStrokeProperty, value); }
        }

    }
}
