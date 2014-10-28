using System;
using System.Collections.Generic;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Nop.Core;
using Nop.Core.Plugins;
using Nop.Services.Cms;
using Nop.Services.Configuration;
using Nop.Services.Localization;

namespace Nop.Plugin.Widgets.NWCustomerCounter
{
    /// <summary>
    /// Live provider
    /// </summary>
    public class NWSocialBarPlugin : BasePlugin, IWidgetPlugin
    {
        private readonly ISettingService _settingService;
        private readonly IWorkContext _workContext;
        private readonly IWebHelper _webHelper;


        private readonly string NAMESPACE = "Nop.Plugin.Widgets.NWCustomerCounter.Controllers";
        private readonly string CONTROLLER_NAME = "WidgetsNWCustomerCounter";
        public NWSocialBarPlugin(ISettingService settingService, IWorkContext workContext, IWebHelper webHelper)
        {
            this._settingService = settingService;
            this._workContext = workContext;
            this._webHelper = webHelper;
        }

        /// <summary>
        /// Gets widget zones where this widget should be rendered
        /// </summary>
        /// <returns>Widget zones</returns>
        public IList<string> GetWidgetZones()
        {
            return new List<string>()
            { 
                "header_menu_after"
            };
        }

        /// <summary>
        /// Gets a route for provider configuration
        /// </summary>
        /// <param name="actionName">Action name</param>
        /// <param name="controllerName">Controller name</param>
        /// <param name="routeValues">Route values</param>
        public void GetConfigurationRoute(out string actionName, out string controllerName,
            out RouteValueDictionary routeValues)
        {
            actionName = "";
            controllerName = "";
            routeValues = new RouteValueDictionary()
            {
                { "Namespaces", "" }, 
                { "area", null }
            };
        }

        /// <summary>
        /// Gets a route for displaying widget
        /// </summary>
        /// <param name="widgetZone">Widget zone where it's displayed</param>
        /// <param name="actionName">Action name</param>
        /// <param name="controllerName">Controller name</param>
        /// <param name="routeValues">Route values</param>
        public void GetDisplayWidgetRoute(string widgetZone, out string actionName, out string controllerName, 
            out RouteValueDictionary routeValues)
        {
            actionName = "PublicInfo";
            controllerName = CONTROLLER_NAME;
            routeValues = new RouteValueDictionary()
            {
                {"Namespaces", NAMESPACE},
                {"area", null},
                {"widgetZone", widgetZone}
            };

        }

        /// <summary>
        /// Install plugin
        /// </summary>
        public override void Install()
        {
            base.Install();
        }

        /// <summary>
        /// Uninstall plugin
        /// </summary>
        public override void Uninstall()
        {
            base.Uninstall();
        }
    }
}
