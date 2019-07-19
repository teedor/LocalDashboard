using DashboardServices;

using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Connectors.NewsApiOrg;
using HelperClasses;
using NSubstitute;

namespace LocalDashboard.UnitTests
{
    [TestFixture]
    public class SpecialRulesEngineTests
    {
        private IDateHelper _dateHelper;
        private SpecialRulesEngine _specialRulesEngine;

        [SetUp]
        public void SetUp()
        {
            _dateHelper = Substitute.For<IDateHelper>();
            _specialRulesEngine = new SpecialRulesEngine(_dateHelper);
        }

        [Test]
        public void ApplyRules_CountryIsNorthKorea_HideNorthKoreaStories()
        {
            // Arrange
            var dashboardModel = new DashboardModel
            {
                CountryCode = "KP",
                NewsArticles = new List<NewsArticle>
                {
                    new NewsArticle
                    {
                        Title = "shit is going down in North Korea",
                        Description = "blah blah"
                    },
                    new NewsArticle
                    {
                        Title = "iuyiuy",
                        Description = "Kim Jong Un is on the rampage in North Korea"
                    },
                    new NewsArticle
                    {
                        Title = "Yahi has no Vanilla Crown Pastries"
                    },
                    new NewsArticle()
                }
            };

            // Act
            _specialRulesEngine.ApplyRules(dashboardModel);

            // Assert
            Assert.AreEqual(2, dashboardModel.NewsArticles.Count);
            Assert.IsFalse(dashboardModel.NewsArticles.Any(x => x.Description != null && x.Description.Contains("North Korea")));
            Assert.IsFalse(dashboardModel.NewsArticles.Any(x => x.Title != null && x.Title.Contains("North Korea")));
        }

        [Test]
        public void ApplyRules_EmptyModel_StillWorks()
        {
            // Arrange
            var dashboardModel = new DashboardModel();

            // Act
            _specialRulesEngine.ApplyRules(dashboardModel);
        }

        [Test]
        public void ApplyRules_NorthKoreaButNoNewArticles_StillWorks()
        {
            // Arrange
            var dashboardModel = new DashboardModel {CountryCode = "KP"};

            // Act
            _specialRulesEngine.ApplyRules(dashboardModel);
        }

        [Test]
        public void ApplyRules_IfTheSunIsNotUp_DisplaySharkWarning()
        {
            // Arrange
            var dashboardModel = new DashboardModel
            {
                Sunrise = new DateTime(2017, 7, 19, 6, 0, 0),
                Sunset = new DateTime(2019, 7, 19, 10, 0, 0),
                LocalTime = new DateTime(2020, 2, 3)
            };

            _dateHelper.IsTheSunUp(Arg.Any<DateTime>(), Arg.Any<DateTime>(), Arg.Any<DateTime>())
                .Returns(false);

            // Act
            _specialRulesEngine.ApplyRules(dashboardModel);

            // Assert
            _dateHelper.Received().IsTheSunUp(dashboardModel.LocalTime, dashboardModel.Sunrise, dashboardModel.Sunset);
            Assert.IsTrue(dashboardModel.SpecialMessages.Any(x => x == "The sun has set so the risk of shark attack is higher"));
        }
    }
}
