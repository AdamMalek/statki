using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using Autofac;
using System.Reflection;
using System.Configuration;
using System.Web.Http;
using BattleShip;

namespace BattleShips
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            if (ConfigurationManager.AppSettings["storeMode"] == "cookie")
            {
                builder.RegisterType<Infrastructure.CookieProvider>().As<Infrastructure.IStoreProvider>();
            }
            else
            {
                builder.RegisterType<Infrastructure.SessionProvider>().As<Infrastructure.IStoreProvider>();
            }

            //builder.RegisterType<Controllers.GameController>().As<Controllers.GameController>();
            builder.RegisterType<Infrastructure.ViewModelBuilder>().As<Infrastructure.ViewModelBuilder>();
            builder.RegisterType<DAL.GameManager>().As<DAL.GameManager>();
            builder.RegisterType<Infrastructure.ShipPositionGenerator>().As<Infrastructure.IShipProvider>();
            
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}
