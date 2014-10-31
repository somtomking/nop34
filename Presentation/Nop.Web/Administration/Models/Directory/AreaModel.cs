using Nop.Web.Framework;
using Nop.Web.Framework.Localization;
using Nop.Web.Framework.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nop.Admin.Models.Directory
{
    public class AreaModel : BaseNopEntityModel, ILocalizedModel<AreaLocalizedModel>
    {
        public AreaModel()
        {
            Locales = new List<AreaLocalizedModel>();
        }
        public int AreaId { get; set; }

        [NopResourceDisplayName("Admin.Configuration.Countries.Areas.Fields.Name")]
        [AllowHtml]
        public string Name { get; set; }

        [NopResourceDisplayName("Admin.Configuration.Countries.Areas.Fields.Abbreviation")]
        [AllowHtml]
        public string Abbreviation { get; set; }

        [NopResourceDisplayName("Admin.Configuration.Countries.Areas.Fields.Published")]
        public bool Published { get; set; }

        [NopResourceDisplayName("Admin.Configuration.Countries.Areas.Fields.DisplayOrder")]
        public int DisplayOrder { get; set; }

        public IList<AreaLocalizedModel> Locales { get; set; }
    }
    public class AreaLocalizedModel : ILocalizedModelLocal
    {
        public int LanguageId { get; set; }

        [NopResourceDisplayName("Admin.Configuration.Countries.Fields.Name")]
        [AllowHtml]
        public string Name { get; set; }

    }
}