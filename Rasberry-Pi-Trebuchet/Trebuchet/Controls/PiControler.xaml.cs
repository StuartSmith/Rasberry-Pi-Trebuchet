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
    public sealed partial class PiControler : UserControl
    {
        public PiControler()
        {
            this.InitializeComponent();
            BrushPanel = new SolidColorBrush(Windows.UI.Colors.Blue);

        }



        public static readonly DependencyProperty BrushPanelProperty = DependencyProperty.Register("BrushPanel", typeof(Brush), typeof(LedLight), new PropertyMetadata(null));
        public Brush BrushPanel
        {
            get { return (Brush)GetValue(BrushPanelProperty); }
            set { SetValue(BrushPanelProperty, value); }
        }

        #region RightLight
        public static readonly DependencyProperty BrushRightLightFillProperty = DependencyProperty.Register("BrushRightLightFill", typeof(Brush), typeof(LedLight), new PropertyMetadata(null));
        public Brush BrushRightLightFill
        {
            get { return (Brush)GetValue(BrushRightLightFillProperty); }
            set { SetValue(BrushRightLightFillProperty, value); }
        }

        public static readonly DependencyProperty BrushRightLightStrokeProperty = DependencyProperty.Register("BrushRightLightFill", typeof(Brush), typeof(LedLight), new PropertyMetadata(null));
        public Brush BrushRightLightStroke
        {
            get { return (Brush)GetValue(BrushRightLightStrokeProperty); }
            set { SetValue(BrushRightLightStrokeProperty, value); }
        }
        #endregion

        #region LeftLight
        public static readonly DependencyProperty BrushLeftLightFillProperty = DependencyProperty.Register("BrushLeftLightFill", typeof(Brush), typeof(LedLight), new PropertyMetadata(null));
        public Brush BrushLeftLightFill
        {
            get { return (Brush)GetValue(BrushLeftLightFillProperty); }
            set { SetValue(BrushLeftLightFillProperty, value); }
        }

        public static readonly DependencyProperty BrushLeftLightStrokeProperty = DependencyProperty.Register("BrushLeftLightFill", typeof(Brush), typeof(LedLight), new PropertyMetadata(null));
        public Brush BrushLeftLightStroke
        {
            get { return (Brush)GetValue(BrushLeftLightStrokeProperty); }
            set { SetValue(BrushLeftLightStrokeProperty, value); }
        }
        #endregion

    }
}
