using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Landmarks.Interfaces;

namespace Landmarks.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMainService _service;

        public HomeController(IMainService service)
        {
            this._service = service;
        }

        public IActionResult Index()
        {
            return View();
        }

        public JsonResult GetRegionInfo(string regionName)
        {
            var regionInfo = this._service.GetRegionInfoByName(regionName);

            if (regionInfo == null) return Json(null);
            var result = Json(regionInfo.Id);
            return result;
        }
            
        [HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            return LocalRedirect(returnUrl);
        }
    }
}
