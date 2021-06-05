using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace FreeCy
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            
            routes.MapRoute(
                name: "JobList",
                url: "Home/ListProduct/",
                defaults: new { controller = "Home", action = "ListProduct" },
                namespaces: new[] { "FreeCy.Controllers" }
            );
            
            routes.MapRoute(
                name: "JobDetail",
                url: "Home/Detail/{productID}",
                defaults: new { controller = "Home", action = "Detail", productID = UrlParameter.Optional },
                namespaces: new[] { "FreeCy.Controllers" }
            );
            routes.MapRoute(
                name: "JobList2",
                url: "Home/ListProduct2/",
                defaults: new { controller = "Home", action = "ListProduct2" },
                namespaces: new[] { "FreeCy.Controllers" }
            );
            routes.MapRoute(
                name: "Default",
                url: "Home/Index/",
                defaults: new { controller = "Home", action = "Index"},
                namespaces: new[] { "FreeCy.Controllers" }
            );
            routes.MapRoute(
                name: "Chat",
                url: "{controller}/{action}",
                defaults: new { controller = "Chat", action = "Index" },
                namespaces: new[] { "FreeCy.Controllers" }
            );

            routes.MapRoute(
                name: "ChatDetail",
                url: "Chat/Details/{iduser}",
                defaults: new { controller = "Chat", action = "Details", iduser = UrlParameter.Optional },
                namespaces: new[] { "FreeCy.Controllers" }
            );


            // routes.MapRoute(
            //    name: "Chat",
            //    url: "{controller}/{action}",
            //    defaults: new { controller = "Chat", action = "Picture" },
            //    namespaces: new[] { "FreeCy.Controllers" }
            //);

        }
    }
}
