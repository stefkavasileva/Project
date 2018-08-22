using Landmarks.Common.Models.Main.ViewModels;
using Landmarks.Interfaces.Main;
using Landmarks.Web.Common.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace Landmarks.Web.Controllers
{
    public class CategoryController : Controller
    {
        private const int DefaultPageSize = 5;
        private readonly ICategoryService _service;

        public CategoryController(ICategoryService service)
        {
            this._service = service;
        }

        [HttpGet]
        public IActionResult List(int? pageIndex)
        {
            var viewModel = this._service.GetCategories();
            int pageSize = DefaultPageSize;

            var result = PaginatedList<ListCategoriesViewModel>.Create(viewModel, pageIndex ?? 1, pageSize);
            return View(result);  
        }

        [HttpGet]
        public IActionResult LandmarksByCategory(int? id)
        {
            if (!id.HasValue) return NotFound();

            var viewModel = this._service.GetLandmarksByCategory(id.Value);

            if (viewModel is null) return NotFound();

            return View(viewModel);
        }
    }
}