using AutoMapper;
using Landmarks.Common.Models.Admin.BindingModels;
using Landmarks.Interfaces.Admin;
using Landmarks.Web.Common.Constants;
using Landmarks.Web.Common.Extensions;
using Landmarks.Web.Common.Messages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Landmarks.Web.Areas.Admin.Pages.Region
{
    [Authorize(Roles = NamesConstants.RoleAdmin)]
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

            var region = this._service.GetRegion(id.Value);

            if (region == null) return NotFound();

            this.EditCategoryBindingModel = this._mapper.Map<AddEditRegionBindingModel>(region);

            return this.Page();
        }

        public IActionResult OnPost(int id)
        {
            if (ModelState.IsValid)
            {
                this.EditCategoryBindingModel.Id = id;
                this._service.SaveEntity(this.EditCategoryBindingModel);

                this.TempData.Put(MessageConstants.Name, new MessageModel()
                {
                    Type = MessageType.Warning,
                    Message = MessageConstants.RegionEditSuccess
                });

                return RedirectToPage(RedirectURL.ToRegionList, new { Area = NamesConstants.AdminArea });
            }

            return this.Page();
        }
    }
}