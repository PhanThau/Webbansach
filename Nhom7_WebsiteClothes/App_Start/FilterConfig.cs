using System.Web;
using System.Web.Mvc;

namespace Nhom7_WebsiteClothes
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
