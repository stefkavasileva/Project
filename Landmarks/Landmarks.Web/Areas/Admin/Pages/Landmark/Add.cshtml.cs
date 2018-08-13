using Landmarks.Common.Models.Admin.BindingModels;
using Landmarks.Interfaces.Admin;
using Landmarks.Web.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Landmarks.Web.Areas.Admin.Pages.Landmark
{
    [Authorize(Roles = "Administrator")]
    public class AddModel : PageModel
    {
        private readonly ILandmarkService _service;

        public AddModel(ILandmarkService service)
        {
            this._service = service;
            this.AddLandmarkBindingModel = new AddEditLandmarkBindingModel();
        }

        [BindProperty]
        public AddEditLandmarkBindingModel AddLandmarkBindingModel { get; set; }

        public void OnGet()
        {
            this._service.FillDropDownItems(AddLandmarkBindingModel);
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                var userId = this.User.GetUserId();
                this.AddLandmarkBindingModel.CreatorId = userId;
                this._service.CreateLandmark(this.AddLandmarkBindingModel);

                return RedirectToPage("/Landmark/List", new { Area = "Admin" });
            }

            this._service.FillDropDownItems(AddLandmarkBindingModel);

            return Page();
        }
    }
}