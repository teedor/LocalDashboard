
using Newtonsoft.Json;
using RestSharp;

namespace Connectors.OpenWeatherMap
{
    public class OpenWeatherMapConnector : IOpenWeatherMapConnector
    {
        private readonly IOpenWeatherMapConnectorSettings _openWeatherMapConnectorSettings;

        public OpenWeatherMapConnector(IOpenWeatherMapConnectorSettings openWeatherMapConnectorSettings)
        {
            _openWeatherMapConnectorSettings = openWeatherMapConnectorSettings;
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
                SunRiseTime = openWeatherMapApiResponseModel.sys.sunrise,
                SunSetTime = openWeatherMapApiResponseModel.sys.sunset
            };

            return result;
        }
    }
}
