using Landmarks.Interfaces.Main;
using Microsoft.AspNetCore.Mvc;

namespace Landmarks.Web.Controllers
{
    public class RegionController : Controller
    {
        private readonly IRegionService _service;

        public RegionController(IRegionService service)
        {
            this._service = service;
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var viewModel = this._service.GetRegionById(id);
            
            return View(viewModel);
        }
    }
}