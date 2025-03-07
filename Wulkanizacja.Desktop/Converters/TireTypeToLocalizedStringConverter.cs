using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using Wulkanizacja.Desktop.Dictionary;
using Wulkanizacja.Desktop.Enums;

namespace Wulkanizacja.Desktop.Converters
{
    class TireTypeToLocalizedStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is TireType tireType)
            {
                string languageCode = CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;
                return TranslationDictionary.Translate(tireType, languageCode);
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
