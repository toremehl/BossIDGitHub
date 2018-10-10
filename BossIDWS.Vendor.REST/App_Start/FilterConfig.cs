using System.Web.Mvc;

#pragma warning disable 1591

namespace BossIDWS.Vendor.REST
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}