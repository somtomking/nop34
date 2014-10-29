using Nop.Web.Framework.Mvc.Routes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace Nop.Plugin.Misc.WebApiServices
{
    public class RouteProvider : IRouteProvider
    {
        public void RegisterRoutes(RouteCollection routes)
        {
            var config = GlobalConfiguration.Configuration;
            WebApiConfig.Register(config);
        }

        public int Priority
        {
            get
            {
                return 0;
            }
        }
    }
}