using System.Collections.Generic;
using System.Linq;
using Landmarks.Common.Models.Admin.ViewModels;
using Landmarks.Interfaces.Admin;
using Landmarks.Web.Common.Constants;
using Landmarks.Web.Common.Extensions;
using Landmarks.Web.Common.Helpers.Messages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Landmarks.Web.Areas.Admin.Pages.Region
{
    [Authorize(Roles = NamesConstants.RoleAdmin)]
    public class ListModel : PageModel
    {
        private readonly IRegionService _service;

        public ListModel(IRegionService service)
        {
            this._service = service;
        }

        public ICollection<RegionConciseViewModel> Regions { get; set; }

        public void OnGet()
        {
            this.Regions = this._service.GetRegions().ToList();
        }

        public IActionResult OnPostDelete(int? id)
        {
            if (id == null) return NotFound();

            var region = this._service.GetRegion(id.Value);

            if (region == null) return NotFound();

            this._service.DeleteRegion(region);

            this.TempData.Put(MessageConstants.Name, new MessageModel()
            {
                Type = MessageType.Danger,
                Message = MessageConstants.RegionDeleteSuccess
            });

            return RedirectToPage(RedirectURL.ToRegionList, new { Area = NamesConstants.AdminArea });
        }
    }
}