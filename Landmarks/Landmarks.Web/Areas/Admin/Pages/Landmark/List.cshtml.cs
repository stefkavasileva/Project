using System.Collections.Generic;
using System.Linq;
using Landmarks.Common.Models.Admin.ViewModels;
using Landmarks.Interfaces.Admin;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Landmarks.Web.Areas.Admin.Pages.Landmark
{
    public class ListModel : PageModel
    {
        private readonly ILandmarkService _service;

        public ListModel(ILandmarkService service)
        {
            this._service = service;
        }

        public ICollection<LandmarkConciseViewModel> Categories { get; set; }

        public void OnGet()
        {
            this.Categories = this._service.GetLandmarks().ToList();    
        }
        public IActionResult OnPostDelete(int? id)
        {
            if (id == null) return NotFound();

            var region = this._service.GetLandmark(id.Value);

            if (region == null) return NotFound();

            this._service.DeleteLandmar(region);

            return RedirectToPage("/Landmark/List");
        }
    }
}