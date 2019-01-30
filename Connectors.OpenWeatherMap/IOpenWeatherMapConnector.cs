﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connectors.OpenWeatherMap
{
    public interface IOpenWeatherMapConnector
    {
        OpenWeatherMapDetails GetOpenWeatherMapDetails(string latitude, string logitude);
    }
}
