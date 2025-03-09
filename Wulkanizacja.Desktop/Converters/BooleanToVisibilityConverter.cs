using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows;

namespace Wulkanizacja.Desktop.Converters
{
    public class BooleanToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool flag)
            {
                if (parameter?.ToString().Equals("Invert", StringComparison.OrdinalIgnoreCase) == true)
                {
                    flag = !flag;
                }
                return flag ? Visibility.Visible : Visibility.Collapsed;
            }
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Visibility visibility)
            {
                bool flag = visibility == Visibility.Visible;
                if (parameter?.ToString().Equals("Invert", StringComparison.OrdinalIgnoreCase) == true)
                {
                    flag = !flag;
                }
                return flag;
            }
            return false;
        }
    }
}
