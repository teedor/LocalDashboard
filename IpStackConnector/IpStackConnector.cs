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
            throw new NotImplementedException();
        }
    }
}
