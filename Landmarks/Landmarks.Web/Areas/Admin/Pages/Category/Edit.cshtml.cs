using System.Threading.Tasks;
using AutoMapper;
using Landmarks.Common.Models.Admin.BindingModels;
using Landmarks.Interfaces.Admin;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Landmarks.Web.Areas.Admin.Pages.Category
{
    public class EditModel : PageModel
    {
        private readonly ICategoryService _service;
        private readonly IMapper _mapper;

        public EditModel(ICategoryService service,IMapper mapper)
        {
            this._service = service;
            this._mapper = mapper;
        }

        [BindProperty]
        public AddEditCategoryBindingModel EditCategoryBindingModel { get; set; }

        public async Task<IActionResult> OnGet(int? id)
        {
            if (id == null) return NotFound();

            var category = await this._service.GetCategoryAsync(id.Value);

            if (category == null) return NotFound();

            this.EditCategoryBindingModel = this._mapper.Map<AddEditCategoryBindingModel>(category);

            return this.Page();
        }

        public async Task<IActionResult> OnPost(int id)
        {
            if (ModelState.IsValid)
            {
                var category = await _service.GetCategoryAsync(id);

                if (category == null) return NotFound();

                category.Name = this.EditCategoryBindingModel.Name;

                this._service.SaveEntity(category);

                return RedirectToPage("/Category/List", new {Area = "Admin" });

            }

            return this.Page();
        }
    }
}