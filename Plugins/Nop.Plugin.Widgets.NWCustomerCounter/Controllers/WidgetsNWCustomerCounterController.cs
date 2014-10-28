using System;
using System.Linq;
using System.Web.Mvc;
using Nop.Core;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.News;
using Nop.Plugin.Widgets.NWCustomerCounter.Models;
using Nop.Services.Catalog;
using Nop.Services.Configuration;
using Nop.Services.Customers;
using Nop.Services.Logging;
using Nop.Services.Orders;
using Nop.Services.Stores;
using Nop.Web.Framework.Controllers;

namespace Nop.Plugin.Widgets.NWCustomerCounter.Controllers
{
    public class WidgetsNWCustomerCounterController : BasePluginController
    {
        private readonly ILogger _logger;
        private readonly ICustomerService _customerService;
        private readonly CustomerSettings _customerSettings;


        public WidgetsNWCustomerCounterController(ILogger logger, ICustomerService customerService, 
            CustomerSettings customerSettings)
        {
            this._customerService = customerService;
            this._logger = logger;
            this._customerSettings = customerSettings;
        }

        [ChildActionOnly]
        public ActionResult PublicInfo(string widgetZone, object additionalData = null)
        {
            var routeData = ((System.Web.UI.Page)this.HttpContext.CurrentHandler).RouteData;
            var model = new PublicInfoModel();
            try
            {
                var controller = routeData.Values["controller"];
                var action = routeData.Values["action"];

                if (controller == null || action == null)
                    return Content("");

                var customers = _customerService.GetOnlineCustomers(DateTime.UtcNow.AddMinutes(-_customerSettings.OnlineCustomerMinutes),
                                null, 0, Int32.MaxValue)
                                .Select(c => c.Id);
                model.TotalCutomers = customers.Count();

            }
            catch (Exception ex)
            {
                _logger.InsertLog(Core.Domain.Logging.LogLevel.Error, "Error creating scripts for NopWarehouse CustomerCounter.", ex.ToString());
            }
            return View("~/Plugins/Widgets.NWCustomerCounter/Views/WidgetsNWCustomerCounter/Public.cshtml", model);
        }
    }
}