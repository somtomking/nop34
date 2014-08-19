using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Web.Framework.Mvc;

namespace Nop.Plugin.Widgets.ProductSpecialSale.Models
{
    public class SpecialSaleStageGroupPubilcModel : BaseNopEntityModel
    {
        public SpecialSaleStageGroupPubilcModel()
        {
            SaleStages = new List<SpecialSaleStagePubilcModel>();
        }

        public string Name { get; set; }
        public int Order { get; set; }
        public IList<SpecialSaleStagePubilcModel> SaleStages { get; set; }
    }
    public class SpecialSaleStagePubilcModel : BaseNopEntityModel
    {
        public DateTime SartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public string ImgUrl { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        public int Order { get; set; }

    }
}
