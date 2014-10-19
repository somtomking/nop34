using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nop.Core.Expand;
using Nop.Admin.Models.CommonExpand;
namespace Nop.Admin.Controllers
{
    [Expand(ExpandType.New, "对CommonController的拓展")]
    public partial class CommonExpandController : BaseAdminController
    {
        public ActionResult RunInfo()
        {
            RunInfoModel model = new RunInfoModel();
            model.RAM = (((double)System.Diagnostics.Process.GetCurrentProcess().WorkingSet64) / 1024) / 1024;
            return View(model);
        }
    }
}