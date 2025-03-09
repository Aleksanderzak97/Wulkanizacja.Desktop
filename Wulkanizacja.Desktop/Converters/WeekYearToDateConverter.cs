using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wulkanizacja.Desktop.Converters
{
    public class WeekYearToDateConverter
    {
        public DateTimeOffset ConvertWeekYearToDate(string weekYear)
        {
            if (string.IsNullOrWhiteSpace(weekYear))
            {
                throw new ArgumentException("Parametr 'weekYear' nie może być pusty ani null.", nameof(weekYear));
            }
            if (weekYear.Length != 4)
                throw new ArgumentException("Format tygodnia i roku powinien mieć 4 znaki, np. '5224'.", nameof(weekYear));

            int week = int.Parse(weekYear.Substring(0, 2));
            int shortYear = int.Parse(weekYear.Substring(2, 2));
            int year = 2000 + shortYear;

            DateTime date = ISOWeek.ToDateTime(year, week, DayOfWeek.Monday);
            return new DateTimeOffset(date, TimeSpan.Zero);
        }

   
        public string ConvertDateToWeekYear(DateTimeOffset? date)
        {
                DateTime utcDate = date.Value.UtcDateTime.Date;

                int week = ISOWeek.GetWeekOfYear(utcDate);
                int year = ISOWeek.GetYear(utcDate);

                int shortYear = year - 2000;
                if (shortYear < 0 || shortYear > 99)
                {
                    throw new ArgumentException("Rok spoza obsługiwanego zakresu (2000-2099).", nameof(date));
                }

                return $"{week:00}{shortYear:00}";
            }
        }
    
    }
}
