using Nop.Core.Plugins;
using Nop.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace Nop.Plugin.Misc.WebApiServices
{
    public class WebServicePlugin : BasePlugin, IMiscPlugin
    {
        #region Methods

        public override void Install()
        {
         
            base.Install();
        }

        public override void Uninstall()
        {
       

            base.Uninstall();
        }

        /// <summary>
        /// Gets a route for provider configuration
        /// </summary>
        /// <param name="actionName">Action name</param>
        /// <param name="controllerName">Controller name</param>
        /// <param name="routeValues">Route values</param>
        public void GetConfigurationRoute(out string actionName, out string controllerName, out RouteValueDictionary routeValues)
        {
            actionName = "Configure";
            controllerName = "MiscWebApiServices";
            routeValues = new RouteValueDictionary() 
            { 
                { "Namespaces", "Nop.Plugin.Misc.WebApiServices.Controllers" },
                { "area", null } 
            };
        }

        #endregion
    }
}