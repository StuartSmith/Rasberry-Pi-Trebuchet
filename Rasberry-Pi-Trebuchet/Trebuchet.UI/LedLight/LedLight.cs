using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
    public sealed class LedLight : Control
    {
        public LedLight()
        {
            this.DefaultStyleKey = typeof(LedLight);
        }



        public Brush LightBrush
        {
            get { return (Brush)GetValue(LightBrushProperty); }
            set { SetValue(LightBrushProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LightBrushProperty =
            DependencyProperty.Register(nameof(LightBrush), typeof(Brush), typeof(LedLight), new PropertyMetadata(new SolidColorBrush(Colors.Red)));

        public Brush LightStroke
        {
            get { return (Brush)GetValue(LightStrokeProperty); }
            set { SetValue(LightStrokeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LightStrokeProperty =
            DependencyProperty.Register(nameof(LightStroke), typeof(Brush), typeof(LedLight), new PropertyMetadata(new SolidColorBrush(Colors.Black)));


    }
}
