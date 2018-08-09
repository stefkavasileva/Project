using AutoMapper;
using Landmarks.Common.Models.Admin.BindingModels;
using Landmarks.Interfaces.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Landmarks.Web.Areas.Admin.Pages.Region
{
    [Authorize(Roles = "Administrator")]
    public class EditModel : PageModel
    {
        private readonly IRegionService _service;
        private readonly IMapper _mapper;

        public EditModel(IRegionService service, IMapper mapper)
        {
            this._service = service;
            this._mapper = mapper;
        }

        [BindProperty]
        public AddEditRegionBindingModel EditCategoryBindingModel { get; set; }

        public IActionResult OnGet(int? id)
        {
            if (id == null) return NotFound();

            var category = this._service.GetRegion(id.Value);

            if (category == null) return NotFound();

            this.EditCategoryBindingModel = this._mapper.Map<AddEditRegionBindingModel>(category);

            return this.Page();
        }

        public IActionResult OnPost(int id)
        {
            if (ModelState.IsValid)
            {
                this.EditCategoryBindingModel.Id = id;
                this._service.SaveEntity(this.EditCategoryBindingModel);

                return RedirectToPage("/Region/List", new { Area = "Admin" });
            }

            return this.Page();
        }
    }
}