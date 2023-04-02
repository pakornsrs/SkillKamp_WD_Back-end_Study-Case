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
    }
}
