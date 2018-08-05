using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Landmarks.Common.Models.Admin.ViewModels;
using Landmarks.Interfaces.Admin;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Landmarks.Web.Areas.Admin.Pages.Category
{
    public class ListModel : PageModel
    {
        private readonly ICategoryService _service;

        public ListModel(ICategoryService service)
        {
            this._service = service;
        }

        public ICollection<CategoryConciseViewModel> Categories { get; set; }

        public void OnGet()
        {
            this.Categories = this._service.GetCategories().ToList();    
        }

        public async Task<IActionResult> OnPostDelete(int? id)
        {
            if (id == null) return NotFound();

            var category = await this._service.GetCategoryAsync(id.Value);

            if (category == null) return NotFound();

            this._service.DeleteCategoryAsync(category);

            return RedirectToPage("/Category/List");
        }
    }
}