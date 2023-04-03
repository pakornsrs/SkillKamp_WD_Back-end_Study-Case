using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Backend.BLL.UtilityMethods
{
    public static class DateTimeUtility
    {
        public static DateTime GetDateTimeThai()
        {
            return DateTime.UtcNow.AddHours(7);
        }

        public static DateTime ConvertUnixToDateTime(long unixtime)
        {
            if (unixtime.ToString().Length < 11)
            {
                // Convert from unixtime second
                DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(unixtime);
                return dateTimeOffset.DateTime;
            }
            else
            {
                // Convert from unixtime millisecond
                DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeMilliseconds(unixtime);
                return dateTimeOffset.DateTime;
            }
        }

        public static string GetUnixTimestamp(int year, int month, int day, int? hour = null, int? min = null, int? sec = null)
        {
            if (hour == null) hour = 0;
            if (min == null) min = 0;
            if (sec == null) sec = 0;
  
            DateTime dateTime = new DateTime(year, month, day, hour.Value, min.Value, sec.Value);
            DateTime unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            TimeSpan timeSpan = dateTime - unixEpoch;
            long unixTimeMilliseconds = (long)timeSpan.TotalMilliseconds;

            return unixTimeMilliseconds.ToString();
        }
    }
}
