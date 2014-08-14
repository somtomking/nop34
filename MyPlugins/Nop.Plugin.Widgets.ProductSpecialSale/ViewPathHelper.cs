using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Widgets.ProductSpecialSale
{
    public class ViewPathHelper
    {
        private readonly static string _Widget_Path_Format = "~/Plugins/Widgets.ProductSpecialSale/Views/WidgetsProductSpecialSale/{0}.cshtml";
        private readonly static string _Page_Path_Format = "~/Plugins/Widgets.ProductSpecialSale/Views/SpecialSaler/{0}.cshtml";

        public static string GetViewPathForWidget(string viewName)
        {
            return string.Format(_Widget_Path_Format, viewName);
        }
        public static string GetViewPathForPage(string viewName)
        {
            return string.Format(_Page_Path_Format, viewName);
        }
    }
}
