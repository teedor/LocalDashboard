using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connectors.IpStack
{
    public class IpStackConnector : IIpStackConnector
    {
        private readonly IIpStackConnectorSettings _ipStackConnectorSettings;

        public IpStackConnector(IIpStackConnectorSettings ipStackConnectorSettings)
        {
            _ipStackConnectorSettings = ipStackConnectorSettings;
        }

        public IpStackDetails GetIpStackDetails(string ipAddress)
        {
            // for localhost testing
            if (ipAddress == "::1")
            {
                ipAddress = "62.31.103.154";
            }

            // using http because free subscription doesn't support https
            // hard-coded base url because it doesn't change
            var restClient = new RestClient("http://api.ipstack.com/");
            var request = new RestRequest($"{ipAddress}?access_key={_ipStackConnectorSettings.IpStackAccessKey}", Method.GET);
            var response = restClient.Execute(request);

            var ipStackApiResponse = JsonConvert.DeserializeObject<IpStackApiResponse>(response.Content);

            var result = new IpStackDetails
            {
                CountryCode = ipStackApiResponse.country_code,
                Latitude = ipStackApiResponse.latitude,
                Longitude = ipStackApiResponse.longitude
            };

            return result;
        }
    }
}
