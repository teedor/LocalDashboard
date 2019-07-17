using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connectors.OpenWeatherMap
{
    public class OpenWeatherMapDetails
    {
        public DateTime SunSetTime { get; set; }
        public DateTime SunRiseTime { get; set; }
        public string Description { get; set; }
        public decimal Temperature { get; set; }
    }
}
