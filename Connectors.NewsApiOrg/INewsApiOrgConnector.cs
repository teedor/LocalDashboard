using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connectors.NewsApiOrg
{
    public interface INewsApiOrgConnector
    {
        List<NewsArticle> GetNewsArticles(string countryCode);
    }
}
