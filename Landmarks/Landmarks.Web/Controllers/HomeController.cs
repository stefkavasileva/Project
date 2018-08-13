﻿using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Landmarks.Web.Models;
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

            if (regionInfo == null) return Json("There's not have information for this region");
            return Json(regionInfo);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
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
