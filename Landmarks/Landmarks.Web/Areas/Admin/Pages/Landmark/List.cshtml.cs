using System.Collections.Generic;
using System.Linq;
using Landmarks.Common.Models.Admin.ViewModels;
using Landmarks.Interfaces.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Landmarks.Web.Areas.Admin.Pages.Landmark
{
    [Authorize(Roles = "Administrator")]
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
            this.Landmarks = this._service.GetLandmarks().ToList();    
        }
        public IActionResult OnPostDelete(int? id)
        {
            if (id == null) return NotFound();

            var region = this._service.GetLandmark(id.Value);

            if (region == null) return NotFound();

            this._service.DeleteLandmark(region);

            return RedirectToPage("/Landmark/List");
        }
    }
}