using Landmarks.Common.Models.Admin.ViewModels;
using Landmarks.Interfaces.Admin;
using Landmarks.Web.Common.Constants;
using Landmarks.Web.Common.Extensions;
using Landmarks.Web.Common.Helpers;
using Landmarks.Web.Common.Messages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Landmarks.Web.Areas.Admin.Pages.Landmark
{
    [Authorize(Roles = NamesConstants.RoleAdmin)]
    public class ListModel : PageModel
    {
        private const int DefaultPageSize = 5;
        private readonly ILandmarkService _service;

        public ListModel(ILandmarkService service)
        {
            this._service = service;
        }

        public PaginatedList<LandmarkConciseViewModel> Landmarks { get; set; }

        public void OnGet(int? pageIndex)
        {
            var viewModel = this._service.GetLandmarks();

            int pageSize = DefaultPageSize;
            this.Landmarks = PaginatedList<LandmarkConciseViewModel>.Create(viewModel, pageIndex ?? 1, pageSize);
        }

        public IActionResult OnPostDelete(int? id)
        {
            if (id == null) return NotFound();

            var landmark = this._service.GetLandmark(id.Value);

            if (landmark == null) return NotFound();

            this._service.DeleteLandmark(landmark);

            this.TempData.Put(MessageConstants.Name, new MessageModel()
            {
                Type = MessageType.Danger,
                Message = MessageConstants.LandmarkDeleteSuccess
            });

            return RedirectToPage(RedirectURL.ToLandmarkList, new { Area = NamesConstants.AdminArea });
        }
    }
}