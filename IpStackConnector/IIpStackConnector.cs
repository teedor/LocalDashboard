using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connectors.IpStack
{
    public interface IIpStackConnector
    {
        IpStackDetails GetIpStackResponse(string ipAddress);
    }
}
