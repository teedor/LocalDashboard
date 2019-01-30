using Connectors.IpStack;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DashboardServices
{
    public class DashboardSettingsWrapper : IIpStackConnectorSettings
    {
        public string IpStackAccessKey => ConfigurationManager.AppSettings["IpStackAccessKey"];
    }
}
