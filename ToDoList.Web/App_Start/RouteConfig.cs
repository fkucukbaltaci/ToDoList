using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ToDoList.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Task",
                url: "Task/{TodoId}",
                defaults:new {controller = "Task", action="Index"}
               );

            routes.MapRoute(
                name: "Task Edit",
                url: "Task/{action}/{TodoId}-{TaskId}",
                defaults: new { controller = "Task", action = "AddEdit" }
               );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Todo", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
