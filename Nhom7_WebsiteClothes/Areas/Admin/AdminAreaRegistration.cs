using System.Web.Mvc;

namespace Nhom7_WebsiteClothes.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { controller = "Clothes", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}