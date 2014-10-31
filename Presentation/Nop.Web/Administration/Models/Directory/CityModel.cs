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
    public class CityModel : BaseNopEntityModel, ILocalizedModel<CityLocalizedModel>
    {
        public CityModel()
        {
            Locales = new List<CityLocalizedModel>();
        }
        public int CityId { get; set; }

        [NopResourceDisplayName("Admin.Configuration.Countries.Cities.Fields.Name")]
        [AllowHtml]
        public string Name { get; set; }

        [NopResourceDisplayName("Admin.Configuration.Countries.Cities.Fields.Abbreviation")]
        [AllowHtml]
        public string Abbreviation { get; set; }

        [NopResourceDisplayName("Admin.Configuration.Countries.Cities.Fields.Published")]
        public bool Published { get; set; }

        [NopResourceDisplayName("Admin.Configuration.Countries.Cities.Fields.DisplayOrder")]
        public int DisplayOrder { get; set; }

        public IList<CityLocalizedModel> Locales { get; set; }
    }
    public class CityLocalizedModel : ILocalizedModelLocal
    {
        public int LanguageId { get; set; }

        [NopResourceDisplayName("Admin.Configuration.Countries.Fields.Name")]
        [AllowHtml]
        public string Name { get; set; }

    }
}