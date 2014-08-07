using Nop.Core;
using Nop.Core.Domain.Catalog;
using Nop.Plugin.Widgets.ProductSpecialSale.Domain;
using Nop.Plugin.Widgets.ProductSpecialSale.Models;
using Nop.Plugin.Widgets.ProductSpecialSale.Services;
using Nop.Services.Catalog;
using Nop.Services.Configuration;
using Nop.Services.Customers;
using Nop.Services.Helpers;
using Nop.Services.Localization;
using Nop.Services.Media;
using Nop.Services.Orders;
using Nop.Services.Vendors;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Nop.Services.Seo;
using System.Globalization;
using Nop.Core.Domain.Media;
using Nop.Core.Domain.Customers;
using Nop.Web.Framework.Kendoui;
using Nop.Services.Stores;
using Nop.Core.Caching;
using AutoMapper;
namespace Nop.Plugin.Widgets.ProductSpecialSale.Controllers
{
    public class WidgetsProductSpecialSaleController : BasePluginController
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

        public WidgetsProductSpecialSaleController(
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
        #region 配置
        [AdminAuthorize]
        [ChildActionOnly]
        public ActionResult Configure()
        {
            return View(GetViewPath("Configure"), new SpecialSaleStageQueryModel());
        }
        [HttpPost]
        [AdminAuthorize]
        [ChildActionOnly]
        public ActionResult Configure(string s)
        {
            return View();
        }
        [ChildActionOnly]
        public ActionResult PublicInfo(string widgetZone)
        {

            return View(GetViewPath("PublicInfo"));
        }

        private string GetViewPath(string viewName)
        {
            return ViewPathHelper.GetViewPath(viewName);
        }
        #endregion



        #region 特卖
        public ActionResult CreateStageGroup()
        {
            return View(GetViewPath("CreateStageGroup"), new SpecialSaleStageGroupModel());
        }
        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public ActionResult CreateStageGroup(SpecialSaleStageGroupModel model, bool continueEditing)
        {
            if (this.ModelState.IsValid)
            {
                var entity = _specialSaleStageService.GetSpecialSaleStageGroupById(model.Id);
                entity = model.ToEntity(entity);
                _specialSaleStageService.CreateSpecialSaleStageGroup(entity);
                var vm = entity.ToModel();
                vm.SaleGroupCreate.SpecialSaleStageGroupId = vm.Id;

                SuccessNotification("添加特卖分组成功!");
                return continueEditing ? RedirectToAction("EditStageGroup", new { id = vm.Id }) : RedirectToAction("List");
            }
            return View(GetViewPath("CreateStageGroup"), model);
        }

        public ActionResult EditStageGroup(int? id)
        {
            if (!id.HasValue)
            {
                return Content("Id is null");
            }
            var data = _specialSaleStageService.GetSpecialSaleStageGroupById(id.Value);
            if (data == null)
            {
                return Content(string.Format("未能找到分组:[ID:{0}]", id));
            }
            var model = data.ToModel();
            model.SaleGroupCreate.SpecialSaleStageGroupId = model.Id;
            model.HasSaleStage = data.SpecialSaleStages.Count() > 0;
            return View(GetViewPath("CreateStageGroup"), model);
        }
        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public ActionResult EditStageGroup(SpecialSaleStageGroupModel model, bool continueEditing)
        {
            if (this.ModelState.IsValid)
            {
                var entity = _specialSaleStageService.GetSpecialSaleStageGroupById(model.Id);
                entity = model.ToEntity(entity);

                _specialSaleStageService.UpdateSpecialSaleStageGroup(entity);
                SuccessNotification("更新特卖分组成功!");
                return continueEditing ? RedirectToAction("EditStageGroup", new { id = model.Id }) : RedirectToAction("List");
            }


            return View(GetViewPath("CreateStageGroup"), model);
        }

        [HttpPost]
        public ActionResult CreateSaleTage(SpecialSaleStageModel.SpecialSaleStageCreateModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.SpecialSaleStageGroupId == 0)
                {
                    return Content("SpecialSaleStageGroupId is set!");
                }
                var group = _specialSaleStageService.GetSpecialSaleStageGroupById(model.SpecialSaleStageGroupId);
                SpecialSaleStage entity = new SpecialSaleStage();
                group.SpecialSaleStages.Add(model.ToEntity(entity));
                _specialSaleStageService.UpdateSpecialSaleStageGroup(group);

                return Json(new { Result = true });
            }
            return Json(new { Result = false });
        }
        [HttpPost]
        public ActionResult SpecialSaleStageList(DataSourceRequest command, SpecialSaleStageQueryModel model)
        {
            var result = _specialSaleStageService.QuerySpecialSaleStage(command.Page - 1, command.PageSize);

            var modelData = new List<SpecialSaleStageGroupModel>();
            foreach (var item in result)
            {
                var vm = new SpecialSaleStageGroupModel();
                vm = AutoMapper.Mapper.Map(item, vm);
                modelData.Add(vm);
            }
            var data = new DataSourceResult()
            {
                Data = modelData,
                Total = result.Count(),
            };
            return Json(data);
        }
        [HttpPost]
        public ActionResult SpecialSaleTageList(DataSourceRequest command, int saleStageGroupId)
        {

            var group = _specialSaleStageService.GetSpecialSaleStageGroupById(saleStageGroupId);
            var modelData = new List<SpecialSaleStageModel.SpecialSaleStageListModel>();
            if (group != null)
            {
                foreach (var item in group.SpecialSaleStages)
                {
                    var model = item.ToModel();

                    SpecialSaleStageModel.SpecialSaleStageListModel lmodel = new SpecialSaleStageModel.SpecialSaleStageListModel();
                    Mapper.DynamicMap(model, lmodel);
                    modelData.Add(lmodel);
                    var defaultProductPicture = _pictureService.GetPictureById(item.PictureId);
                    lmodel.ImagePath = _pictureService.GetPictureUrl(defaultProductPicture, 75, true);
                    lmodel.SourceImagePath = _pictureService.GetPictureUrl(defaultProductPicture);

                }
            }

            var data = new DataSourceResult()
            {
                Data = modelData,
                Total = modelData.Count,
            };
            return Json(data);

        }
        [HttpPost]
        public ActionResult SpecialSaleTageProductList(DataSourceRequest command, int? saleStageId)
        {
            DataSourceResult data = null;
            IPagedList<SpecialSaleProduct> list = null;
            if (!saleStageId.HasValue || saleStageId.Value < 1)
            {
                list = new PagedList<SpecialSaleProduct>(new List<SpecialSaleProduct>(), command.Page - 1, command.PageSize);
            }
            else
            {
                list = _specialSaleStageService.GetSpecialSaleStageProductList(saleStageId.Value, command.Page - 1, command.PageSize);
            }
            var vmList = new List<SpecialSaleProductModel>();
            foreach (var item in list)
            {
                var vm = new SpecialSaleProductModel();
                Mapper.DynamicMap(item, vm);
                var product = item.GetProduct();
                if (product != null)
                {
                    vm.ProductName = product.Name;
                    vm.Sku = product.Sku;
                }
                else
                {
                    vm.ProductName = "未找到该产品！";
                }

                vmList.Add(vm);
            }
            data = new DataSourceResult()
          {
              Data = vmList,
              Total = list.TotalCount,
          };

            return Json(data);

        }

        public ActionResult GetSimpleSpecialSaleTageListBySaleGroupId(int? saleGroupId)
        {
            IList<SpecialSaleStage> saleStageList = null;
            if (!saleGroupId.HasValue || saleGroupId.Value < 1)
            {
                saleStageList = new List<SpecialSaleStage>();

            }
            else
            {
                saleStageList = _specialSaleStageService.GetSpecialSaleStageBySaleGroupId(saleGroupId.Value);
            }

            return Json(saleStageList.Select(s => new { Id = s.Id, Name = s.Name }), JsonRequestBehavior.AllowGet);
        }

        public ActionResult SaleStagePop(int? saleStageId)
        {
            if (!saleStageId.HasValue)
            {
                return Content("saleStageId can not null!");
            }
            var model = new SpecialSaleProductModel.AddSpecialSaleProductModel { SaleStageId = saleStageId.Value };


            //categories
            model.AvailableCategories.Add(new SelectListItem() { Text = _localizationService.GetResource("Admin.Common.All"), Value = "0" });
            var categories = _categoryService.GetAllCategories(showHidden: true);
            foreach (var c in categories)
                model.AvailableCategories.Add(new SelectListItem() { Text = c.GetFormattedBreadCrumb(categories), Value = c.Id.ToString() });

            //manufacturers
            model.AvailableManufacturers.Add(new SelectListItem() { Text = _localizationService.GetResource("Admin.Common.All"), Value = "0" });
            foreach (var m in _manufacturerService.GetAllManufacturers(showHidden: true))
                model.AvailableManufacturers.Add(new SelectListItem() { Text = m.Name, Value = m.Id.ToString() });

            //stores
            model.AvailableStores.Add(new SelectListItem() { Text = _localizationService.GetResource("Admin.Common.All"), Value = "0" });
            foreach (var s in _storeService.GetAllStores())
                model.AvailableStores.Add(new SelectListItem() { Text = s.Name, Value = s.Id.ToString() });

            //vendors
            model.AvailableVendors.Add(new SelectListItem() { Text = _localizationService.GetResource("Admin.Common.All"), Value = "0" });
            foreach (var v in _vendorService.GetAllVendors(0, int.MaxValue, true))
                model.AvailableVendors.Add(new SelectListItem() { Text = v.Name, Value = v.Id.ToString() });

            //product types
            model.AvailableProductTypes = ProductType.SimpleProduct.ToSelectList(false).ToList();
            model.AvailableProductTypes.Insert(0, new SelectListItem() { Text = _localizationService.GetResource("Admin.Common.All"), Value = "0" });

            return View(GetViewPath("SaleStagePop"), model);
        }

        [HttpPost]
        [FormValueRequired("save")]
        public ActionResult SaleStagePop(string btnId, string formId, SpecialSaleProductModel.AddSpecialSaleProductModel model)
        {
            var saleStage = _specialSaleStageService.GetSpecialSaleStageById(model.SaleStageId);
            if (model.SelectedProductIds != null)
            {
                foreach (var pId in model.SelectedProductIds)
                {
                    var p = _productService.GetProductById(pId);

                    saleStage.SpecialSaleProducts.Add(new SpecialSaleProduct()
                    {
                        DisplayOrder = 0,
                        OriginalPrice = p.Price,
                        Quantity = model.Quantity,
                        Price = model.Price,
                        ProdcutId = pId
                    });

                }
                _specialSaleStageService.UpdateSpecialSaleStageGroup(saleStage.SpecialSaleStageGroup);
            }
            ViewBag.RefreshPage = true;
            ViewBag.btnId = btnId;
            ViewBag.formId = formId;
            return View(GetViewPath("SaleStagePop"), model);
        }

        #endregion


    }
}
