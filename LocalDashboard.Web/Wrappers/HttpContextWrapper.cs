using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LocalDashboard.Web.Wrappers
{
    public class HttpContextWrapper : IHttpContextWrapper
    {
        public string IpAddress => HttpContext.Current.Request.UserHostAddress;
    }
}