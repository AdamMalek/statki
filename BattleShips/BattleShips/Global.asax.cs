﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac.Integration.Mvc;
using Autofac;
using System.Reflection;
using System.Configuration;

namespace BattleShips
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
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

            builder.RegisterType<DAL.GameManager>().As<DAL.GameManager>();
            builder.RegisterType<Infrastructure.ShipPositionGenerator>().As<Infrastructure.IShipProvider>();
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}