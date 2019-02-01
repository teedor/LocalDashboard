using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DashboardServices
{
    public class NewsSummary
    {
        public string Source { get; set; }
        public string Url { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string PublishedDateLocalTime { get; set; }
    }
}
