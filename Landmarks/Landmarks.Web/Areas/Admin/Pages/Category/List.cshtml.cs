using System.Collections.Generic;
using System.Linq;
using Landmarks.Common.Models.Admin.ViewModels;
using Landmarks.Interfaces.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Landmarks.Web.Areas.Admin.Pages.Category
{
    [Authorize(Roles = "Administrator")]
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

        public IActionResult OnPostDelete(int? id)
        {
            if (id == null) return NotFound();

            var category =  this._service.GetCategory(id.Value);

            if (category == null) return NotFound();

            this._service.DeleteCategory(category);

            return RedirectToPage("/Category/List");
        }
    }
}