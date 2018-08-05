using System.Collections.Generic;
using System.Linq;
using Landmarks.Common.Models.Admin.ViewModels;
using Landmarks.Interfaces.Admin;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Landmarks.Web.Areas.Admin.Pages.Region
{
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

            var region =  this._service.GetRegion(id.Value);

            if (region == null) return NotFound();

            this._service.DeleteRegion(region);

            return RedirectToPage("/Region/List");
        }
    }
}