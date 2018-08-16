using System.Collections.Generic;
using Landmarks.Common.Models.Admin.BindingModels;
using Landmarks.Interfaces.Admin;
using Landmarks.Web.Common.Constants;
using Landmarks.Web.Common.Extensions;
using Landmarks.Web.Common.Helpers.Messages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Landmarks.Web.Areas.Admin.Pages.Region
{
    [Authorize(Roles = NamesConstants.RoleAdmin)]
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

                this.TempData.Put(MessageConstants.Name, new MessageModel()
                {
                    Type = MessageType.Success,
                    Message = MessageConstants.RegionSuccess
                });

                return RedirectToPage(RedirectURL.ToRegionList, new { Area = NamesConstants.AdminArea });
            }

            return Page();
        }
    }
}