using DashboardModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DashboardServices
{
    public interface IDashboardService
    {
        DashboardModel GetDashboardModel(string ipAddress);
    }
}
