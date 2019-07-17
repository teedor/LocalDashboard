using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connectors.NewsApiOrg
{
    public class NewsApiOrgConnector : INewsApiOrgConnector
    {
        private readonly INewsApiOrgConnectorSettings _newsApiOrgConnectorSettings;

        public NewsApiOrgConnector(INewsApiOrgConnectorSettings newsApiOrgConnectorSettings)
        {
            _newsApiOrgConnectorSettings = newsApiOrgConnectorSettings;
        }

        public List<NewsArticle> GetNewsArticles(string countryCode, int gmtOffset)
        {
            throw new NotImplementedException();
        }
    }
}
