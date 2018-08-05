using Microsoft.AspNetCore.Mvc;

namespace Landmarks.Web.Areas.Admin.Controllers
{
    public class HomeController : AdminController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}