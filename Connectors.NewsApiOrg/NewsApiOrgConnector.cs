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

        public List<NewsArticle> GetNewsArticles(string countryCode)
        {
            var restClient = new RestClient("https://newsapi.org/v2/");
            var request = new RestRequest($"top-headlines?country={countryCode}&apiKey={_newsApiOrgConnectorSettings.NewsApiOrgApiKey}", Method.GET);
            var response = restClient.Execute(request);
            var newsApiOrgApiResponseModel = JsonConvert.DeserializeObject<NewsApiOrgApiResponseModel>(response.Content);

            var result = new List<NewsArticle>();
            foreach (var item in newsApiOrgApiResponseModel.articles)
            {
                result.Add(new NewsArticle
                {
                    Source = item.source.name,
                    Url = item.url,
                    Title = item.title,
                    Description = item.description,
                    PublishedDateLocalTime = Convert.ToDateTime(item.publishedAt)
                });
            }

            return result;
        }
    }
}
