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
        public List<NewsArticle> NewsArticles { get; set; }
        public string CountryCode { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public DateTime Sunrise { get; set; }
        public DateTime Sunset { get; set; }
        public List<string> SpecialMessages { get; set; }
    }
}
