using Landmarks.Common.Models.Admin.BindingModels;
using Landmarks.Interfaces.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Landmarks.Web.Areas.Admin.Pages.Category
{
    [Authorize(Roles = "Administrator")]
    public class AddModel : PageModel
    {
        private readonly ICategoryService _service;

        public AddModel(ICategoryService service)
        {
            this._service = service;
        }

        [BindProperty]
        public AddEditCategoryBindingModel AddCategoryBindingModel { get; set; }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                this._service.CreateCategory(this.AddCategoryBindingModel.Name);

                return RedirectToPage("/Category/List", new { Area = "Admin" });
            }

            return Page();
        }

        //[HttpPost]
        //public JsonResult IsCateggoryNameExist(string name)
        //{
        //    var user = this._service.GetCategoryByName(name);
        //    return new JsonResult(user == null);
        //}

    }
}