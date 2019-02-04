using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelperClasses
{
    public interface IDateHelper
    {
        DateTime UnixIntToDateTime(int unixDateInteger);

        bool IsTheSunUp(DateTime localTime, DateTime sunrise, DateTime sunset);
    }
}
