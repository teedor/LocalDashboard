using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelperClasses
{
    public class DateHelper : IDateHelper
    {
        public DateTime UnixIntToDateTime(int unixDateInteger)
        {
            DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixDateInteger).ToLocalTime();
            return dtDateTime;
        }

        public bool IsTheSunUp(DateTime localTime, DateTime sunrise, DateTime sunset)
        {
            return localTime >= sunrise && localTime < sunset;
        }
    }
}
