using System.Globalization;

namespace Eshop.Extensions
{
    public static class DateExtensions
    {
        public static string ToPersianDate(this DateTime dateTime)
        {
            PersianCalendar persianCalendar = new PersianCalendar();
            string year = persianCalendar.GetYear(dateTime).ToString();
            string month = persianCalendar.GetMonth(dateTime).ToString()
                           .PadLeft(2, '0');
            string day = persianCalendar.GetDayOfMonth(dateTime).ToString()
                           .PadLeft(2, '0');
            string hour = dateTime.Hour.ToString().PadLeft(2, '0');
            string minute = dateTime.Minute.ToString().PadLeft(2, '0');
            string second = dateTime.Second.ToString().PadLeft(2, '0');
            return String.Format("{0}/{1}/{2} {3}:{4}:{5}", year, month, day, hour, minute, second);
        }
    }
}
