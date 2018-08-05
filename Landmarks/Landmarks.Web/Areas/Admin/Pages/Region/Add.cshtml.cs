using Landmarks.Common.Models.Admin.BindingModels;
using Landmarks.Interfaces.Admin;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Landmarks.Web.Areas.Admin.Pages.Region
{
    public class AddModel : PageModel
    {
        private readonly IRegionService _service;

        public AddModel(IRegionService service)
        {
            this._service = service;
        }

        [BindProperty]
        public AddEditRegionBindingModel AddRegionBindingModel { get; set; }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                 this._service.CreateRegion(this.AddRegionBindingModel);

                return RedirectToPage("/Region/List", new { Area = "Admin" });
            }

            return Page();
        }
    }
}