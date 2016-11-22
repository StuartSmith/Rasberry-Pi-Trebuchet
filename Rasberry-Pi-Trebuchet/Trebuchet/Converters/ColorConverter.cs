using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace Trebuchet.Converters
{
    public class StringToColorConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {

            if (targetType.IsConstructedGenericType && targetType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                if (value == null)
                {
                    return null;
                }
                targetType = Nullable.GetUnderlyingType(targetType);
            }

            if (targetType.Name=="Brush")
            {
                if (value == null)
                    return new SolidColorBrush(Windows.UI.Colors.AntiqueWhite);

                return GetSolidColorBrush(value.ToString());
            }

            return System.Convert.ChangeType(value, targetType);

        }

        public static SolidColorBrush GetSolidColorBrush(string hex)
        {
           
            hex = hex.Replace("#", string.Empty);
            byte a = (byte)(System.Convert.ToUInt32(hex.Substring(0, 2), 16));
            byte r = (byte)(System.Convert.ToUInt32(hex.Substring(2, 2), 16));
            byte g = (byte)(System.Convert.ToUInt32(hex.Substring(4, 2), 16));
            byte b = (byte)(System.Convert.ToUInt32(hex.Substring(6, 2), 16));
            SolidColorBrush myBrush = new SolidColorBrush(Windows.UI.Color.FromArgb(a, r, g, b));
            return myBrush;
        }




        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return Convert(value, targetType, parameter, language);
        }
    }
}
