using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BabyApp_Server
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
               name: "Default",
               url: "",
               defaults: new { controller = "BabyServer", action = "Login", id = UrlParameter.Optional }
           );

            routes.MapRoute(
                name: "BabyServer",
                url: "{action}",
                defaults: new { controller = "BabyServer", action = "Add", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default1",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "BabyServer", action = "Index", id = UrlParameter.Optional }
            );

            
        }
    }
}