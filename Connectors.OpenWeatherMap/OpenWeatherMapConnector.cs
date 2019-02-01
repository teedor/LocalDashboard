using HelperClasses;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connectors.OpenWeatherMap
{
    public class OpenWeatherMapConnector : IOpenWeatherMapConnector
    {
        private readonly IOpenWeatherMapConnectorSettings _openWeatherMapConnectorSettings;
        private readonly IDateHelper _dateHelper;

        public OpenWeatherMapConnector(IOpenWeatherMapConnectorSettings openWeatherMapConnectorSettings, IDateHelper dateHelper)
        {
            _openWeatherMapConnectorSettings = openWeatherMapConnectorSettings;
            _dateHelper = dateHelper;
        }

        public OpenWeatherMapDetails GetOpenWeatherMapDetails(string latitude, string longitude)
        {
            var restClient = new RestClient("https://api.openweathermap.org/data/2.5/weather");
            var request = new RestRequest($"?lat={latitude}&lon={longitude}&APPID={_openWeatherMapConnectorSettings.OpenWeatherMapApiKey}&units=metric", Method.GET);
            var response = restClient.Execute(request);
            var openWeatherMapApiResponseModel = JsonConvert.DeserializeObject<OpenWeatherMapApiResponseModel>(response.Content);

            var result = new OpenWeatherMapDetails
            {
                Description = openWeatherMapApiResponseModel.weather[0].main,
                Temperature = openWeatherMapApiResponseModel.main.temp,
                SunRiseTime = _dateHelper.UnixIntToDateTime(openWeatherMapApiResponseModel.sys.sunrise),
                SunSetTime = _dateHelper.UnixIntToDateTime(openWeatherMapApiResponseModel.sys.sunset)
            };

            return result;
        }
    }
}
