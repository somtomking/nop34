using System.Web.Mvc;
using Nop.Core;
using Nop.Core.Caching;
using Nop.Plugin.Widgets.ProductSpecialSale.Services;
using Nop.Services.Catalog;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Media;
using Nop.Services.Stores;
using Nop.Services.Vendors;
using Nop.Web.Framework.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Widgets.ProductSpecialSale.Controllers
{
    public class SpecialSalerController : BasePluginController
    {
        private readonly IWorkContext _workContext;
        private readonly IStoreContext _storeContext;
        private readonly IStoreService _storeService;
        private readonly IPictureService _pictureService;
        private readonly ISettingService _settingService;
        private readonly IProductService _productService;
        private readonly ICacheManager _cacheManager;
        private readonly ISpecialSaleStageService _specialSaleStageService;
        private readonly ICategoryService _categoryService;
        private readonly IManufacturerService _manufacturerService;
        private readonly ILocalizationService _localizationService;
        private readonly IVendorService _vendorService;
        public SpecialSalerController(
         IWorkContext workContext,
            IStoreContext storeContext,
            IStoreService storeService,
            IPictureService pictureService,
            ISettingService settingService,
            ICacheManager cacheManager,
           IProductService productService,
            ISpecialSaleStageService specialSaleStageService,
            ICategoryService categoryService,
            IManufacturerService manufacturerService,
            ILocalizationService localizationService,
            IVendorService vendorService
            )
        {
            this._workContext = workContext;
            this._storeContext = storeContext;
            this._storeService = storeService;
            this._pictureService = pictureService;
            this._settingService = settingService;
            this._cacheManager = cacheManager;
            this._specialSaleStageService = specialSaleStageService;
            this._productService = productService;
            this._categoryService = categoryService;
            this._manufacturerService = manufacturerService;
            this._localizationService = localizationService;
            this._vendorService = vendorService;
        }


        #region 卖场

        public ActionResult Index(int? id)
        {
            ViewBag.Id = id;
          

            return View(ViewPathHelper.GetViewPathForPage("Index"));

        }

        #endregion
    }
}
