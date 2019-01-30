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

        public OpenWeatherMapConnector(IOpenWeatherMapConnectorSettings openWeatherMapConnectorSettings)
        {
            _openWeatherMapConnectorSettings = openWeatherMapConnectorSettings;
        }

        public OpenWeatherMapDetails GetOpenWeatherMapDetails(string latitude, string logitude)
        {
            throw new NotImplementedException();
        }
    }
}
