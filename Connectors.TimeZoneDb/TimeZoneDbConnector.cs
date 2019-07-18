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

            //var restClient = new RestClient("https://api.timezonedb.com/v2.1/");
            //var request = new RestRequest($"get-time-zone?key={_timeZoneDbConnectorSettings.TimeZoneDbApiKey}&by=position&lat={latitude}&format=json&lng={longitude}", Method.GET);
            //var response = restClient.Execute(request);
            //var timeZoneDbApiResponseModel = JsonConvert.DeserializeObject<TimeZoneDbApiResponseModel>(response.Content);

            //var result = new TimeZoneDbDetails
            //{
            //    LocalTime = _dateHelper.UnixIntToDateTime(timeZoneDbApiResponseModel.timestamp),
            //    GmtOffset = timeZoneDbApiResponseModel.gmtOffset
            //};

            //return result;
        }
    }
}
