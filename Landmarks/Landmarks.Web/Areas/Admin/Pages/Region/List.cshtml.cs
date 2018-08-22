using Landmarks.Common.Models.Admin.ViewModels;
using Landmarks.Interfaces.Admin;
using Landmarks.Web.Common.Constants;
using Landmarks.Web.Common.Extensions;
using Landmarks.Web.Common.Helpers;
using Landmarks.Web.Common.Messages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Landmarks.Web.Areas.Admin.Pages.Region
{
    [Authorize(Roles = NamesConstants.RoleAdmin)]
    public class ListModel : PageModel
    {
        private const int DefaultPageSize = 5;
        private readonly IRegionService _service;

        public ListModel(IRegionService service)
        {
            this._service = service;
        }

        public PaginatedList<RegionConciseViewModel> Regions { get; set; }

        public void OnGet(int? pageIndex)
        {
            var viewModel = this._service.GetRegions();

            int pageSize = DefaultPageSize;
            this.Regions = PaginatedList<RegionConciseViewModel>.Create(viewModel, pageIndex ?? 1, pageSize);
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