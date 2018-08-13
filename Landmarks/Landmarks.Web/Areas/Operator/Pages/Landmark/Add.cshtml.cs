using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
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
    public class AddModel : PageModel
    {
        private readonly ILandmarkService _service;
        private readonly IHostingEnvironment _hostingEnvironment;

        public AddModel(ILandmarkService service, IHostingEnvironment hostingEnvironment)
        {
            this._service = service;
            this._hostingEnvironment = hostingEnvironment;
            this.AddLandmarkBindingModel = new AddEditLandmarkBindingModel();
        }

        [BindProperty]
        public AddEditLandmarkBindingModel AddLandmarkBindingModel { get; set; }

        public void OnGet()
        {
            this._service.FillDropDownItems(AddLandmarkBindingModel);
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                //from extension method
                var userId = this.User.GetUserId();
                this.AddLandmarkBindingModel.CreatorId = userId;

                var images = this.AddLandmarkBindingModel.Images;
                var imagesPaths = new List<string>();

                foreach (var image in images)
                {
                    if (image.Length <= 0) continue;

                    var newFileName = userId + Path.GetFileName(image.FileName);
                    //TODO add validation for extension            
                    var fullFilePath = Path.Combine(_hostingEnvironment.WebRootPath + "/images/", newFileName);

                    using (var fileStram = new FileStream(fullFilePath, FileMode.Create))
                    {
                        await image.CopyToAsync(fileStram);
                    }

                    imagesPaths.Add($"~/images/{newFileName}");
                }

                this._service.AddLandmark(this.AddLandmarkBindingModel, imagesPaths);
                return RedirectToPage("/Landmark/List", new { Area = "Operator" });
            }

            this._service.FillDropDownItems(AddLandmarkBindingModel);

            return Page();
        }
    }
}