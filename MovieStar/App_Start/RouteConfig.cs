using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MovieStar
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Film_by_Genre",
                url: "Film/Genre/{genre}",
                defaults: new { controller = "Film", action = "Genre" }
            );

            routes.MapRoute(
                name: "Film_by_Director",
                url: "Film/Director/{director}",
                defaults: new { controller = "Film", action = "Director" }
            );

            routes.MapRoute(
                name: "Film_by_Category",
                url: "Film/Category/{category}",
                defaults: new { controller = "Film", action = "Category" }
            );

            routes.MapRoute(
                name: "Film_by_Country",
                url: "Film/Country/{country}",
                defaults: new { controller = "Film", action = "Country" }
            );

            routes.MapRoute(
                name: "Film_by_Dublaj",
                url: "Film/Dublaj/{dublaj}",
                defaults: new { controller = "Film", action = "Dublaj" }
            );

            routes.MapRoute(
                name: "Film_by_Actor",
                url: "Film/Actor/{actor}",
                defaults: new { controller = "Film", action = "Actor" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
