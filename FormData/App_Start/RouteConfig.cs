﻿using System.Web.Mvc;
using System.Web.Routing;

namespace FormData
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "ProductsByCategory",
                url: "categories/{id}/products",
                defaults: new { controller = "Product", action = "ProductByCategory" }
            );
            routes.MapRoute(
                name: "SpecificProduct",
                url: "products/{id}",
                defaults: new { controller = "Product", action = "Index" }
            );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
