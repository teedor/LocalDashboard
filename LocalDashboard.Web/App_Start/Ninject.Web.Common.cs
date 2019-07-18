[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(LocalDashboard.Web.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(LocalDashboard.Web.App_Start.NinjectWebCommon), "Stop")]

namespace LocalDashboard.Web.App_Start
{
    using System;
    using System.Web;
    using Connectors.IpStack;
    using Connectors.NewsApiOrg;
    using Connectors.OpenWeatherMap;
    using Connectors.TimeZoneDb;
    using DashboardServices;
    using HelperClasses;
    using LocalDashboard.Web.Wrappers;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using Ninject.Web.Common.WebHost;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();
                RegisterServices(kernel);
                System.Web.Http.GlobalConfiguration.Configuration.DependencyResolver = new Ninject.WebApi.DependencyResolver.NinjectDependencyResolver(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<IHttpContextWrapper>().To<Wrappers.HttpContextWrapper>().InSingletonScope();
            kernel.Bind<IDashboardService>().To<DashboardService>().InSingletonScope();
            kernel.Bind<IIpStackConnector>().To<IpStackConnector>().InSingletonScope();
            kernel.Bind<INewsApiOrgConnector>().To<NewsApiOrgConnector>().InSingletonScope();
            kernel.Bind<IOpenWeatherMapConnector>().To<OpenWeatherMapConnector>().InSingletonScope();
            kernel.Bind<ITimeZoneDbConnector>().To<TimeZoneDbConnector>().InSingletonScope();
            kernel.Bind<IDateHelper>().To<DateHelper>().InSingletonScope();
            //kernel.Bind<IIpStackConnectorSettings>().To<DashboardSettingsWrapper>().InSingletonScope();
            //kernel.Bind<INewsApiOrgConnectorSettings>().To<DashboardSettingsWrapper>().InSingletonScope();
            //kernel.Bind<IOpenWeatherMapConnectorSettings>().To<DashboardSettingsWrapper>().InSingletonScope();
            //kernel.Bind<ITimeZoneDbConnectorSettings>().To<DashboardSettingsWrapper>().InSingletonScope();
        }        
    }
}