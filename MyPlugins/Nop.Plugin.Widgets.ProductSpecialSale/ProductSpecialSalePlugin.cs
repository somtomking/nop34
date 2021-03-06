﻿using Nop.Core;
using Nop.Core.Plugins;
using Nop.Plugin.Widgets.ProductSpecialSale.Data;
using Nop.Plugin.Widgets.ProductSpecialSale.Services;
using Nop.Services.Cms;
using Nop.Services.Configuration;
using Nop.Services.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Routing;

namespace Nop.Plugin.Widgets.ProductSpecialSale
{
    public class ProductSpecialSalePlugin : BasePlugin, IWidgetPlugin
    {
        private readonly IPictureService _pictureService;
        private readonly ISettingService _settingService;
        private readonly IWebHelper _webHelper;
        private readonly ProductSpecialSaleObjectContext _objectContext;
        private readonly ISpecialSaleStageService _specialSaleStageService;
        public ProductSpecialSalePlugin(
            IPictureService pictureService,
              ISettingService settingService, IWebHelper webHelper, ProductSpecialSaleObjectContext objectContext, ISpecialSaleStageService specialSaleStageService
            )
        {
            this._pictureService = pictureService;
            this._settingService = settingService;
            this._webHelper = webHelper;
            this._objectContext = objectContext;
            this._specialSaleStageService = specialSaleStageService;
        }
        public IList<string> GetWidgetZones()
        {
            return new string[] { "home_page_productspecialsale" };
        }

        public void GetConfigurationRoute(out string actionName, out string controllerName, out System.Web.Routing.RouteValueDictionary routeValues)
        {
            actionName = "Configure";
            controllerName = "WidgetsProductSpecialSale";
            routeValues = new RouteValueDictionary() { { "Namespaces", "Nop.Plugin.Widgets.ProductSpecialSale" }, { "area", null } };
        }

        public void GetDisplayWidgetRoute(string widgetZone, out string actionName, out string controllerName, out RouteValueDictionary routeValues)
        {
            actionName = "PublicInfo";
            controllerName = "WidgetsProductSpecialSale";
            routeValues = new RouteValueDictionary()
            {
                {"Namespaces", "Nop.Plugin.Widgets.ProductSpecialSale"},
                {"area", null},
                {"widgetZone", widgetZone}
            };
        }
        public override void Install()
        {
            _objectContext.Install();
            base.Install();

        }
        public override void Uninstall()
        {
            foreach (var pId in _specialSaleStageService.GetReferencePictures())
            {
                var pic = _pictureService.GetPictureById(pId);
                _pictureService.DeletePicture(pic);
            }
            _objectContext.Uninstall();
            base.Uninstall();

        }
    }
}
