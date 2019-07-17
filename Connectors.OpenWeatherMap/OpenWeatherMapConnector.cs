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

        public OpenWeatherMapDetails GetOpenWeatherMapDetails(string latitude, string longitude, int gmtOffset)
        {
            throw new NotImplementedException();
        }
    }
}
