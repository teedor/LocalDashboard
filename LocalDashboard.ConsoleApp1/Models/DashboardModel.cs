using System;
using System.Collections.Generic;

namespace LocalDashboard.ConsoleApp1.Models
{
    public class DashboardModel
    {
        public string IpAddress { get; set; }
        public DateTime LocalTime { get; set; }
        public string WeatherDescription { get; set; }
        public decimal Temperature { get; set; }
        public string NewsMessage { get; set; }
        public string WeatherMessage { get; set; }
        public List<NewsSummary> NewsSummaries { get; set; }
    }
}
