using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Wulkanizacja.Desktop.Converters
{
    public class LoadIndexToIndexWithLoadConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is String loadIndex)
            {
                switch(loadIndex)
                {
                    #region 60-69
                    case "60":
                        return $"{loadIndex} - 250 kg";
                    case "61":
                        return $"{loadIndex} - 257 kg";
                    case "62":
                        return $"{loadIndex} - 265 kg";
                    case "63":
                        return $"{loadIndex} - 272 kg";
                    case "64":
                        return $"{loadIndex} - 280 kg";
                    case "65":
                        return $"{loadIndex} - 290 kg";
                    case "66":
                        return $"{loadIndex} - 300 kg";
                    case "67":
                        return $"{loadIndex} - 307 kg";
                    case "68":
                        return $"{loadIndex} - 315 kg";
                    case "69":
                        return $"{loadIndex} - 325 kg";
                    #endregion
                    #region 70-79
                    case "70":
                        return $"{loadIndex} - 335 kg";
                    case "71":
                        return $"{loadIndex} - 345 kg";
                    case "72":
                        return $"{loadIndex} - 355 kg";
                    case "73":
                        return $"{loadIndex} - 365 kg";
                    case "74":
                        return $"{loadIndex} - 375 kg";
                    case "75":
                        return $"{loadIndex} - 387 kg";
                    case "76":
                        return $"{loadIndex} - 400 kg";
                    case "77":
                        return $"{loadIndex} - 412 kg";
                    case "78":
                        return $"{loadIndex} - 425 kg";
                    case "79":
                        return $"{loadIndex} - 437 kg";
                    #endregion
                    #region 80-89
                    case "80":
                        return $"{loadIndex} - 450 kg";
                    case "81":
                        return $"{loadIndex} - 462 kg";
                    case "82":
                        return $"{loadIndex} - 475 kg";
                    case "83":
                        return $"{loadIndex} - 487 kg";
                    case "84":
                        return $"{loadIndex} - 500 kg";
                    case "85":
                        return $"{loadIndex} - 515 kg";
                    case "86":
                        return $"{loadIndex} - 530 kg";
                    case "87":
                        return $"{loadIndex} - 545 kg";
                    case "88":
                        return $"{loadIndex} - 560 kg";
                    case "89":
                        return $"{loadIndex} - 580 kg";
                    #endregion
                    #region 90-99
                    case "90":
                        return $"{loadIndex} - 600 kg";
                    case "91":
                        return $"{loadIndex} - 615 kg";
                    case "92":
                        return $"{loadIndex} - 630 kg";
                    case "93":
                        return $"{loadIndex} - 650 kg";
                    case "94":
                        return $"{loadIndex} - 670 kg";
                    case "95":
                        return $"{loadIndex} - 690 kg";
                    case "96":
                        return $"{loadIndex} - 710 kg";
                    case "97":
                        return $"{loadIndex} - 730 kg";
                    case "98":
                        return $"{loadIndex} - 750 kg";
                    case "99":
                        return $"{loadIndex} - 775 kg";
                    #endregion
                    #region 100-109
                    case "100":
                        return $"{loadIndex} - 800 kg";
                    case "101":
                        return $"{loadIndex} - 825 kg";
                    case "102":
                        return $"{loadIndex} - 850 kg";
                    case "103":
                        return $"{loadIndex} - 875 kg";
                    case "104":
                        return $"{loadIndex} - 900 kg";
                    case "105":
                        return $"{loadIndex} - 925 kg";
                    case "106":
                        return $"{loadIndex} - 950 kg";
                    case "107":
                        return $"{loadIndex} - 975 kg";
                    case "108":
                        return $"{loadIndex} - 1000 kg";
                    case "109":
                        return $"{loadIndex} - 1030 kg";
                    #endregion
                    #region 110-119
                    case "110":
                        return $"{loadIndex} - 1060 kg";
                    case "111":
                        return $"{loadIndex} - 1090 kg";
                    case "112":
                        return $"{loadIndex} - 1120 kg";
                    case "113":
                        return $"{loadIndex} - 1150 kg";
                    case "114":
                        return $"{loadIndex} - 1180 kg";
                    case "115":
                        return $"{loadIndex} - 1215 kg";
                    case "116":
                        return $"{loadIndex} - 1250 kg";
                    case "117":
                        return $"{loadIndex} - 1285 kg";
                    case "118":
                        return $"{loadIndex} - 1320 kg";
                    case "119":
                        return $"{loadIndex} - 1360 kg";
                    #endregion
                    #region 120-129
                    case "120":
                        return $"{loadIndex} - 1400 kg";
                    case "121":
                        return $"{loadIndex} - 1450 kg";
                    case "122":
                        return $"{loadIndex} - 1500 kg";
                    case "123":
                        return $"{loadIndex} - 1550 kg";
                    case "124":
                        return $"{loadIndex} - 1600 kg";
                    case "125":
                        return $"{loadIndex} - 1650 kg";
                    case "126":
                        return $"{loadIndex} - 1700 kg";
                    case "127":
                        return $"{loadIndex} - 1750 kg";
                    case "128":
                        return $"{loadIndex} - 1800 kg";
                    case "129":
                        return $"{loadIndex} - 1850 kg";
                    #endregion
                    #region 130-139
                    case "130":
                        return $"{loadIndex} - 1900 kg";
                    case "131":
                        return $"{loadIndex} - 1950 kg";
                    case "132":
                        return $"{loadIndex} - 2000 kg";
                    case "133":
                        return $"{loadIndex} - 2060 kg";
                    case "134":
                        return $"{loadIndex} - 2120 kg";
                    case "135":
                        return $"{loadIndex} - 2180 kg";
                    case "136":
                        return $"{loadIndex} - 2240 kg";
                    case "137":
                        return $"{loadIndex} - 2300 kg";
                    case "138":
                        return $"{loadIndex} - 2360 kg";
                    case "139":
                        return $"{loadIndex} - 2430 kg";
                    #endregion
                    #region 140-149
                    case "140":
                        return $"{loadIndex} - 2500 kg";
                    case "141":
                        return $"{loadIndex} - 2575 kg";
                    case "142":
                        return $"{loadIndex} - 2650 kg";
                    case "143":
                        return $"{loadIndex} - 2725 kg";
                    case "144":
                        return $"{loadIndex} - 2800 kg";
                    case "145":
                        return $"{loadIndex} - 2900 kg";
                    case "146":
                        return $"{loadIndex} - 3000 kg";
                    case "147":
                        return $"{loadIndex} - 3075 kg";
                    case "148":
                        return $"{loadIndex} - 3150 kg";
                    case "149":
                        return $"{loadIndex} - 3250 kg";
                    #endregion
                    #region 150-159
                    case "150":
                        return $"{loadIndex} - 3350 kg";
                    case "151":
                        return $"{loadIndex} - 3450 kg";
                    case "152":
                        return $"{loadIndex} - 3550 kg";
                    case "153":
                        return $"{loadIndex} - 3650 kg";
                    case "154":
                        return $"{loadIndex} - 3750 kg";
                    case "155":
                        return $"{loadIndex} - 3875 kg";
                    case "156":
                        return $"{loadIndex} - 4000 kg";
                    case "157":
                        return $"{loadIndex} - 4125 kg";
                    case "158":
                        return $"{loadIndex} - 4250 kg";
                    case "159":
                        return $"{loadIndex} - 4375 kg";
                    #endregion
                    #region 160-170
                    case "160":
                        return $"{loadIndex} - 4500 kg";
                    case "161":
                        return $"{loadIndex} - 4625 kg";
                    case "162":
                        return $"{loadIndex} - 4750 kg";
                    case "163":
                        return $"{loadIndex} - 4875 kg";
                    case "164":
                        return $"{loadIndex} - 5000 kg";
                    case "165":
                        return $"{loadIndex} - 5150 kg";
                    case "166":
                        return $"{loadIndex} - 5300 kg";
                    case "167":
                        return $"{loadIndex} - 5450 kg";
                    case "168":
                        return $"{loadIndex} - 5600 kg";
                    case "169":
                        return $"{loadIndex} - 5800 kg";
                    case "170":
                        return $"{loadIndex} - 6000 kg";
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
