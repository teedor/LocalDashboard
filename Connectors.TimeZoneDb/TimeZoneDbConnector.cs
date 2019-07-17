using HelperClasses;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connectors.TimeZoneDb
{
    public class TimeZoneDbConnector : ITimeZoneDbConnector
    {
        public IDateHelper _dateHelper;
        public ITimeZoneDbConnectorSettings _timeZoneDbConnectorSettings;

        public TimeZoneDbConnector(IDateHelper dateHelper, ITimeZoneDbConnectorSettings timeZoneDbConnectorSettings)
        {
            _dateHelper = dateHelper;
            _timeZoneDbConnectorSettings = timeZoneDbConnectorSettings;
        }

        public TimeZoneDbDetails GetTimeZoneDbDetails(string latitude, string longitude)
        {
            throw new NotImplementedException();
        }
    }
}
