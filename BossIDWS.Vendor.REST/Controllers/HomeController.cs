using System.Web.Mvc;

#pragma warning disable 1591

namespace BossIDWS.Vendor.REST.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }
    }
}