using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Connectors.NewsApiOrg;

namespace DashboardServices
{
    public class DashboardModel
    {
        public string IpAddress { get; set; }
        public DateTime LocalTime { get; set; }
        public string WeatherDescription { get; set; }
        public decimal Temperature { get; set; }
        public string NewsMessage { get; set; }
        public string WeatherMessage { get; set; }
        public List<NewsArticle> NewsArticles { get; set; }
    }
}
