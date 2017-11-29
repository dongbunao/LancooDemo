using LancooDemo.Interface;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace LancooDemo
{
    public class WebApiApplication : System.Web.HttpApplication
    {
       

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();


            //+
            GlobalConfiguration.Configuration.Services.Replace(typeof(IHttpControllerSelector), new VersionHttpControllerSelector(GlobalConfiguration.Configuration));
            GlobalConfiguration.Configure(Web.WebApiConfig.Register);


           
            
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);


            var factory = new StdSchedulerFactory();
            //scheduler
            IScheduler scheduler = factory.GetScheduler();

            scheduler.Start();


        }
    }
}
