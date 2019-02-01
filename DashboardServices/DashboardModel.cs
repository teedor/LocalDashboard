using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DashboardServices
{
    public class DashboardModel
    {
        public DateTime LocalTime { get; set; }
        public List<NewsSummary> NewsSummaries { get; set; }
        public string WeatherDescription { get; set; }
        public decimal Temperature { get; set; }
    }
}
