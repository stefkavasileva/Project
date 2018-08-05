using Landmarks.Common.Models.Admin.BindingModels;
using Landmarks.Interfaces.Admin;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Landmarks.Web.Areas.Admin.Pages.Landmark
{
    public class AddModel : PageModel
    {
        private readonly ILandmarkService _service;

        public AddModel(ILandmarkService service)
        {
            this._service = service;
            this.AddRegionBindingModel = new AddEditLandmarkBindingModel();
        }

        [BindProperty]
        public AddEditLandmarkBindingModel AddRegionBindingModel { get; set; }

        public void OnGet()
        {
            this._service.FillDropDownItems(AddRegionBindingModel);
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                 this._service.CreateLandmark(this.AddRegionBindingModel);

                return RedirectToPage("/Landmark/List", new {Area = "Admin" });
            }

            this._service.FillDropDownItems(AddRegionBindingModel);

            return Page();
        }
    }
}