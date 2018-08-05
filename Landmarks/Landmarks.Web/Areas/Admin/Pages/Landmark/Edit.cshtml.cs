using AutoMapper;
using Landmarks.Common.Models.Admin.BindingModels;
using Landmarks.Interfaces.Admin;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace Landmarks.Web.Areas.Admin.Pages.Landmark
{
    public class EditModel : PageModel
    {
        private readonly ILandmarkService _service;
        private readonly IMapper _mapper;

        public EditModel(ILandmarkService service,IMapper mapper)
        {
            this._service = service;
            this._mapper = mapper;
        }

        [BindProperty]
        public AddEditLandmarkBindingModel EditLandmarkBindingModel { get; set; }

        public IActionResult OnGet(int? id)
        {
            if (id == null) return NotFound();

            var landmark = this._service.GetLandmark(id.Value);

            if (landmark == null) return NotFound();

            this.EditLandmarkBindingModel = this._mapper.Map<Landmarks.Models.Landmark,AddEditLandmarkBindingModel>(landmark);
            this._service.FillDropDownItems(this.EditLandmarkBindingModel);

            return this.Page();
        }

        public IActionResult OnPost(int id)
        {
            if (ModelState.IsValid)
            {
                this.EditLandmarkBindingModel.Id = id;
                this._service.SaveEntity(this.EditLandmarkBindingModel);

                return RedirectToPage("/Category/List", new {Area = "Admin"});
            }

            return this.Page();
        }
    }
}