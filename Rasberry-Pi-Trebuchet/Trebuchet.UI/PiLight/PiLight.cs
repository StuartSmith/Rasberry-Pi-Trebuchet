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
    public sealed class PiLight : Control
    {
        public PiLight()
        {
            this.DefaultStyleKey = typeof(PiLight);
        }

        protected override void OnApplyTemplate()
        {
            try
            {
                base.OnApplyTemplate();
            }
            catch (Exception ex)
            {
            }
        }

        #region LedLightLeft

        public Brush BrushLeftLightFill
        {
            get { return (Brush)GetValue(BrushLeftLightFillProperty); }
            set { SetValue(BrushLeftLightFillProperty, value); }
        }

        // Using a DependencyProperty as the backing store for BrushLeftLightFill.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BrushLeftLightFillProperty =
            DependencyProperty.Register(nameof(BrushLeftLightFill), typeof(Brush), typeof(PiLight), new PropertyMetadata(new SolidColorBrush(Colors.Blue)));


        public Brush BrushLeftLightStroke
        {
            get { return (Brush)GetValue(BrushLeftLightStrokeProperty); }
            set { SetValue(BrushLeftLightStrokeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for BrushLeftLightFill.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BrushLeftLightStrokeProperty =
            DependencyProperty.Register(nameof(BrushLeftLightStroke), typeof(Brush), typeof(PiLight), new PropertyMetadata(new SolidColorBrush(Colors.Black)));

        #endregion


        #region LedLightRight
        public Brush BrushRightLightFill
        {
            get { return (Brush)GetValue(BrushRightLightFillProperty); }
            set { SetValue(BrushRightLightFillProperty, value); }
        }
        // Using a DependencyProperty as the backing store for BrushRightLightFill.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BrushRightLightFillProperty =
            DependencyProperty.Register(nameof(BrushRightLightFill), typeof(Brush), typeof(PiLight), new PropertyMetadata(new SolidColorBrush(Colors.Blue)));

        public Brush BrushRightLightStroke
        {
            get { return (Brush)GetValue(BrushRightLightStrokeProperty); }
            set { SetValue(BrushRightLightStrokeProperty, value); }
        }
        // Using a DependencyProperty as the backing store for BrushRightLightFill.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BrushRightLightStrokeProperty =
            DependencyProperty.Register(nameof(BrushRightLightStroke), typeof(Brush), typeof(PiLight), new PropertyMetadata(new SolidColorBrush(Colors.Black)));
        #endregion
            

        #region BrushPanel
        public Brush BrushPanel
        {
            get { return (Brush)GetValue(BrushPanelProperty); }
            set { SetValue(BrushPanelProperty, value); }
        }
        // Using a DependencyProperty as the backing store for BrushPanel.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BrushPanelProperty =
            DependencyProperty.Register(nameof(BrushPanel), typeof(Brush), typeof(PiLight), new PropertyMetadata(new SolidColorBrush(Colors.Blue)));
        #endregion

        #region Commands
        public ICommand BothLightsOn
        {
            get { return (ICommand)GetValue(BothLightsOnProperty); }
            set { SetValue(BothLightsOnProperty, value); }
        }       
        public static readonly DependencyProperty BothLightsOnProperty =
            DependencyProperty.Register(nameof(BothLightsOn), typeof(ICommand), typeof(PiLight), new PropertyMetadata(null));


        public ICommand BothLightsOff
        {
            get { return (ICommand)GetValue(BothLightsOffProperty); }
            set { SetValue(BothLightsOffProperty, value); }
        }       
        public static readonly DependencyProperty BothLightsOffProperty =
            DependencyProperty.Register(nameof(BothLightsOff), typeof(ICommand), typeof(PiLight), new PropertyMetadata(null));


        public ICommand LeftLightOff
        {
            get { return (ICommand)GetValue(LeftLightOffProperty); }
            set { SetValue(LeftLightOffProperty, value); }
        }        
        public static readonly DependencyProperty LeftLightOffProperty =
            DependencyProperty.Register(nameof(LeftLightOff), typeof(ICommand), typeof(PiLight), new PropertyMetadata(null));


        public ICommand LeftLightOn
        {
            get { return (ICommand)GetValue(LeftLightOnProperty); }
            set { SetValue(LeftLightOnProperty, value); }
        }       
        public static readonly DependencyProperty LeftLightOnProperty =
            DependencyProperty.Register(nameof(LeftLightOn), typeof(ICommand), typeof(PiLight), new PropertyMetadata(null));


        public ICommand RightLightOn
        {
            get { return (ICommand)GetValue(RightLightOnProperty); }
            set { SetValue(RightLightOnProperty, value); }
        }       
        public static readonly DependencyProperty RightLightOnProperty =
            DependencyProperty.Register(nameof(RightLightOn), typeof(ICommand), typeof(PiLight), new PropertyMetadata(null));

        public ICommand RightLightOff
        {
            get { return (ICommand)GetValue(RightLightOffProperty); }
            set { SetValue(RightLightOffProperty, value); }
        }
        public static readonly DependencyProperty RightLightOffProperty =
            DependencyProperty.Register(nameof(RightLightOff), typeof(ICommand), typeof(PiLight), new PropertyMetadata(null));

        #endregion

    }
}
