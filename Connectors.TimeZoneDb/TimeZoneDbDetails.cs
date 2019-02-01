using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connectors.TimeZoneDb
{
    public class TimeZoneDbDetails
    {
        public int GmtOffset { get; set; }
        public DateTime LocalTime { get; set; }
    }
}
