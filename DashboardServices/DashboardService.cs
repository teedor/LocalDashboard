using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DashboardModels;

namespace DashboardServices
{
    public class DashboardService : IDashboardService
    {
        public DashboardModel GetDashboardModel(string ipAddress)
        {
            return new DashboardModel { LocalTime = DateTime.Now };
        }
    }
}
