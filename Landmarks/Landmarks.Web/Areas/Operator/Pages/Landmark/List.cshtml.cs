using System.Collections.Generic;
using System.Linq;
using Landmarks.Common.Models.Operator.ViewModels;
using Landmarks.Interfaces.Operator;
using Landmarks.Web.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Landmarks.Web.Areas.Operator.Pages.Landmark
{
    [Authorize(Roles = "DataEntryOperator,Administrator")]
    public class ListModel : PageModel
    {
        private readonly ILandmarkService _service;

        public ListModel(ILandmarkService service)
        {
            this._service = service;
        }

        public ICollection<LandmarkConciseViewModel> Landmarks { get; set; }

        public void OnGet()
        {
            var userId = this.User.GetUserId();
            this.Landmarks = this._service.GetLandmarksByCreatorId(userId).ToList();
        }

        public IActionResult OnPostDelete(int? id)
        {
            if (id == null) return NotFound();

            var landmark = this._service.GetLandmark(id.Value);

            if (landmark == null) return NotFound();

            this._service.DeleteLandmark(landmark);

            return RedirectToPage("/Landmark/List", new { Area = "Operator" });
        }
    }
}