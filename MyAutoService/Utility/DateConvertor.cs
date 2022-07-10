using System.Globalization;

namespace MyAutoService.Utility
{
    public static class DateConvertor
    {
        public static string ToShamsi(this DateTime valueTime)
        {
            PersianCalendar persianCalendar = new PersianCalendar();
            return persianCalendar.GetYear(valueTime) + "/" +
                   persianCalendar.GetMonth(valueTime).ToString("00") + "/" +
                   persianCalendar.GetDayOfMonth(valueTime).ToString("00");
        }
    }
}
