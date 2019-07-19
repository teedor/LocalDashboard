using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HelperClasses;

namespace DashboardServices
{
    public class SpecialRulesEngine : ISpecialRulesEngine
    {
        private readonly IDateHelper _dateHelper;

        public SpecialRulesEngine(IDateHelper dateHelper)
        {
            _dateHelper = dateHelper;
        }

        public void ApplyRules(DashboardModel dashboardModel)
        {
            if (dashboardModel.CountryCode == "KP" && dashboardModel.NewsArticles != null)
            {
                var northKoreaArticles = dashboardModel.NewsArticles.Where(x => x.Title != null && x.Title.Contains("North Korea") || x.Description != null && x.Description.Contains("North Korea"));
                dashboardModel.NewsArticles = dashboardModel.NewsArticles.Except(northKoreaArticles).ToList();
            }

            if (!_dateHelper.IsTheSunUp(dashboardModel.LocalTime, dashboardModel.Sunrise, dashboardModel.Sunset))
            {
                if (dashboardModel.SpecialMessages == null)
                {
                    dashboardModel.SpecialMessages = new List<string>();
                }

                dashboardModel.SpecialMessages.Add("The sun has set so the risk of shark attack is higher");
            }
        }
    }
}
