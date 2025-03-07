using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Wulkanizacja.Desktop.Converters
{
    public class SpeedIndexToIndexWithSpeedConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is String speedIndex)
            {
                switch (speedIndex)
                {
                    #region A1 - A8
                    case "A1":
                        return $"{speedIndex} - 5 km/h";
                    case "A2":
                        return $"{speedIndex} - 10 km/h";
                    case "A3":
                        return $"{speedIndex} - 15 km/h";
                    case "A4":
                        return $"{speedIndex} - 20 km/h";
                    case "A5":
                        return $"{speedIndex} - 25 km/h";
                    case "A6":
                        return $"{speedIndex} - 30 km/h";
                    case "A7":
                        return $"{speedIndex} - 35 km/h";
                    case "A8":
                        return $"{speedIndex} - 40 km/h";
                    #endregion
                    #region B - Y
                    case "B":
                        return $"{speedIndex} - 50 km/h";
                    case "C":
                        return $"{speedIndex} - 60 km/h";
                    case "D":
                        return $"{speedIndex} - 65 km/h";
                    case "E":
                        return $"{speedIndex} - 70 km/h";
                    case "F":
                        return $"{speedIndex} - 80 km/h";
                    case "G":
                        return $"{speedIndex} - 90 km/h";
                    case "J":
                        return $"{speedIndex} - 100 km/h";
                    case "K":
                        return $"{speedIndex} - 110 km/h";
                    case "L":
                        return $"{speedIndex} - 120 km/h";
                    case "M":
                        return $"{speedIndex} - 130 km/h";
                    case "N":
                        return $"{speedIndex} - 140 km/h";
                    case "P":
                        return $"{speedIndex} - 150 km/h";
                    case "Q":
                        return $"{speedIndex} - 160 km/h";
                    case "R":
                        return $"{speedIndex} - 170 km/h";
                    case "S":
                        return $"{speedIndex} - 180 km/h";
                    case "T":
                        return $"{speedIndex} - 190 km/h";
                    case "U":
                        return $"{speedIndex} - 200 km/h";
                    case "H":
                        return $"{speedIndex} - 210 km/h";
                    case "V":
                        return $"{speedIndex} - 240 km/h";
                    case "W":
                        return $"{speedIndex} - 270 km/h";
                    case "Y":
                        return $"{speedIndex} - 300 km/h";
                        #endregion
                }

            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
