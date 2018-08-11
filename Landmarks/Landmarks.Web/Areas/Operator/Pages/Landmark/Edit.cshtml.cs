using System.Linq;
using AutoMapper;
using Landmarks.Common.Models.Operator.BindingModels;
using Landmarks.Interfaces.Operator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace Landmarks.Web.Areas.Operator.Pages.Landmark
{
    [Authorize(Roles = "DataEntryOperator")]
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
            
            //TODO add mapper
            //this.EditLandmarkBindingModel = this._mapper.Map<Landmarks.Models.Landmark,AddEditLandmarkBindingModel>(landmark);
            this.EditLandmarkBindingModel = new AddEditLandmarkBindingModel
            {
                Name = landmark.Name,
                ImagesPathToDisplay = landmark.Images.Select(i=> i.Path).ToList(),
                CategoryId = landmark.CategoryId,
                Description = landmark.Description,
                RegionId = landmark.RegionId               
            };

            this._service.FillDropDownItems(this.EditLandmarkBindingModel);

            return this.Page();
        }

        //public IActionResult OnPost(int id)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        this.EditLandmarkBindingModel.Id = id;
        //        this._service.SaveEntity(this.EditLandmarkBindingModel);

        //        return RedirectToPage("/Landmark/List", new {Area = "Admin"});
        //    }

        //    return this.Page();
        //}
    }
}