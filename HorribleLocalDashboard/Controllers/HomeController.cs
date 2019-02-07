using HorribleLocalDashboard.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace HorribleLocalDashboard.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var ipAddress = Request.UserHostAddress;

            // for localhost testing
            if (ipAddress == "::1")
            {
                ipAddress = "62.31.103.154";
            }

            // get location details from ipstack
            var restClient = new RestClient("http://api.ipstack.com/");
            var request = new RestRequest($"{ipAddress}?access_key=bd72f9d78cdf9a9d4e86ce94f588469e", Method.GET);
            var response = restClient.Execute(request);

            var ipStackApiResponse = JsonConvert.DeserializeObject<IpStackApiResponse>(response.Content);

            var countryCode = ipStackApiResponse.country_code;
            var latitude = ipStackApiResponse.latitude;
            var longitude = ipStackApiResponse.longitude;

            var isNorthKorea = countryCode == "KP";

            // get timezone details
            restClient = new RestClient("https://api.timezonedb.com/v2.1/");
            request = new RestRequest($"get-time-zone?key=G74B8EFI4L4Y&by=position&lat={latitude}&format=json&lng={longitude}", Method.GET);
            response = restClient.Execute(request);
            var timeZoneDbApiResponseModel = JsonConvert.DeserializeObject<TimeZoneDbApiResponseModel>(response.Content);

            var localTime = UnixIntToDateTime(timeZoneDbApiResponseModel.timestamp);
            var gmtOffset = timeZoneDbApiResponseModel.gmtOffset;

            // get the weather details
            restClient = new RestClient("https://api.openweathermap.org/data/2.5/weather");
            request = new RestRequest($"?lat={latitude}&lon={longitude}&APPID=b9c9e442efabb293bcce54beecabe2d1&units=metric", Method.GET);
            response = restClient.Execute(request);
            var openWeatherMapApiResponseModel = JsonConvert.DeserializeObject<OpenWeatherMapApiResponseModel>(response.Content);

            var weatherDescription = openWeatherMapApiResponseModel.weather[0].main;
            var temperature = openWeatherMapApiResponseModel.main.temp;
            var sunRiseTime = UnixIntToDateTime(openWeatherMapApiResponseModel.sys.sunrise).AddSeconds(gmtOffset);
            var sunSetTime = UnixIntToDateTime(openWeatherMapApiResponseModel.sys.sunset).AddSeconds(gmtOffset);

            // get news
            List<NewsSummary> newsSummaries = null;
            if (!isNorthKorea)
            {
                restClient = new RestClient("https://newsapi.org/v2/");
                request = new RestRequest($"top-headlines?country={countryCode}&apiKey=f19d1933bff54a48858e88f01d45f1d8", Method.GET);
                response = restClient.Execute(request);
                var newsApiOrgApiResponseModel = JsonConvert.DeserializeObject<NewsApiOrgApiResponseModel>(response.Content);

                newsSummaries = new List<NewsSummary>();
                foreach (var item in newsApiOrgApiResponseModel.articles)
                {
                    newsSummaries.Add(new NewsSummary
                    {
                        Source = item.source.name,
                        Url = item.url,
                        Title = item.title,
                        Description = item.description,
                        PublishedDateLocalTime = Convert.ToDateTime(item.publishedAt).AddSeconds(gmtOffset).ToString("yyyy-MM-dd HH:mm:ss")
                    });
                } 
            }

            // is the sun up?
            var isTheSunUp = IsTheSunUp(localTime, sunRiseTime, sunSetTime);

            // build the viewModel
            var model = new DashboardModel
            {
                IpAddress = ipAddress,
                LocalTime = localTime,
                Temperature = temperature,
                WeatherDescription = weatherDescription,
                NewsMessage = isNorthKorea ? "It would be pointless to display the news because it would be censored propaganda anyway." : string.Empty,
                WeatherMessage = isTheSunUp ? string.Empty : "The sun has set so the risk of shark attack is higher",
                NewsSummaries = newsSummaries
            };

            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        private DateTime UnixIntToDateTime(int unixDateInteger)
        {
            DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixDateInteger).ToLocalTime();
            return dtDateTime;
        }

        private bool IsTheSunUp(DateTime localTime, DateTime sunrise, DateTime sunset)
        {
            return localTime >= sunrise && localTime < sunset;
        }
    }
}