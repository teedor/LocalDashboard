using System;

namespace Connectors.NewsApiOrg
{
    public class NewsArticle
    {
        public string Source { get; set; }
        public string Url { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime PublishedDateLocalTime { get; set; }
    }
}