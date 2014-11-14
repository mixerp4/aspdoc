/********************************************************************************
Copyright (C) Binod Nirvan, Mix Open Foundation (http://mixof.org).

This file is part of ASPDoc.Net.

ASPDoc.Net is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

ASPDoc.Net is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with ASPDoc.Net.  If not, see <http://www.gnu.org/licenses/>.
***********************************************************************************/
using System.Web.Mvc;
using System.Web.Routing;

namespace ASPDoc.Net
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(name: "SignIn", url: "home/signin", defaults: new { controller = "Home", action = "SignIn", id = UrlParameter.Optional });
            routes.MapRoute(name: "SignOut", url: "home/signout", defaults: new { controller = "Home", action = "SignOut", id = UrlParameter.Optional });
            routes.MapRoute(name: "SaveConfiguration", url: "home/configuration/save", defaults: new { controller = "Home", action = "SaveConfiguration", id = UrlParameter.Optional });
            routes.MapRoute(name: "Default", url: "{*url}", defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional });
        }
    }
}