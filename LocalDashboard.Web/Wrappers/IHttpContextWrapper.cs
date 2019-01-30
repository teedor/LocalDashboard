using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalDashboard.Web.Wrappers
{
    public interface IHttpContextWrapper
    {
        string IpAddress { get; }
    }
}
