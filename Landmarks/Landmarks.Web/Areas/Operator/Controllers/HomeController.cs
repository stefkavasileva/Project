using Microsoft.AspNetCore.Mvc;

namespace Landmarks.Web.Areas.Operator.Controllers
{
    public class HomeController : OperatorController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}