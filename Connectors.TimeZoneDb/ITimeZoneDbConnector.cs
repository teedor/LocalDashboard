using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connectors.TimeZoneDb
{
    public interface ITimeZoneDbConnector
    {
        TimeZoneDbDetails GetTimeZoneDbDetails(string latitude, string longitude);
    }
}
