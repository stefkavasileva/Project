using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Landmarks.Common.Models.Operator.BindingModels;
using Landmarks.Interfaces.Operator;
using Landmarks.Web.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Landmarks.Web.Areas.Operator.Pages.Landmark
{
    [Authorize(Roles = "DataEntryOperator,Administrator")]
    public class EditModel : PageModel
    {
        private readonly ILandmarkService _service;
        private readonly IMapper _mapper;
        private readonly IHostingEnvironment _hostingEnvironment;

        public EditModel(ILandmarkService service, IMapper mapper, IHostingEnvironment hostingEnvironment)
        {
            this._service = service;
            this._mapper = mapper;
            this._hostingEnvironment = hostingEnvironment;
        }

        [BindProperty]
        public AddEditLandmarkBindingModel EditLandmarkBindingModel { get; set; }

        public IActionResult OnGet(int? id)
        {
            if (id == null) return NotFound();

            var landmark = this._service.GetLandmark(id.Value);

            if (landmark == null) return NotFound();

            var userId = this.User.GetUserId();
            if (landmark.CreatorId != userId) return RedirectToPage("/Landmark/List", new { Area = "Operator" });
            //TODO add mapper
            //this.EditLandmarkBindingModel = this._mapper.Map<Landmarks.Models.Landmark,AddEditLandmarkBindingModel>(landmark);
            this.EditLandmarkBindingModel = new AddEditLandmarkBindingModel
            {
                Name = landmark.Name,
                ImagesPathToDisplay = landmark.Images.Select(i => i.Path).ToList(),
                CategoryId = landmark.CategoryId,
                Description = landmark.Description,
                RegionId = landmark.RegionId
            };

            this._service.FillDropDownItems(this.EditLandmarkBindingModel);

            return this.Page();
        }

        public async Task<IActionResult> OnPost(int id)
        {
            if (ModelState.IsValid)
            {
                var userId = this.User.GetUserId();
                var images = this.EditLandmarkBindingModel.Images;

                var imagesPaths = new List<string>();

                foreach (var image in images)
                {
                    if (image.Length <= 0)
                    {
                        continue;
                    }
                    var newFileName = userId + Path.GetFileName(image.FileName);
                    //TODO add validation for extension            
                    var fullFilePath = Path.Combine(_hostingEnvironment.WebRootPath + "/images/", newFileName);

                    using (var fileStram = new FileStream(fullFilePath, FileMode.Create))
                    {
                        await image.CopyToAsync(fileStram);
                    }

                    imagesPaths.Add($"~/images/{newFileName}");
                }

                this.EditLandmarkBindingModel.Id = id;

                this._service.SaveEntity(this.EditLandmarkBindingModel, imagesPaths);

                return RedirectToPage("/Landmark/List", new { Area = "Operator" });
            }

            return this.Page();
        }
    }
}