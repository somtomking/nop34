using Nop.Web.Framework.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nop.Plugin.Misc.WebApiServices.Controllers
{
    [AdminAuthorize]
    public class MiscWebApiServicesController : BasePluginController
    {
        public ActionResult Configure()
        {
            return View("~/Plugins/Misc.WebApiServices/Views/MiscWebApiServices/Configure.cshtml");
        }
    }
}