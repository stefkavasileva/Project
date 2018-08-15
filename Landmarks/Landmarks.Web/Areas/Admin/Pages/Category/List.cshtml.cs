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

namespace Landmarks.Web.Areas.Admin.Pages.Category
{
    [Authorize(Roles = NamesConstants.RoleAdmin)]
    public class ListModel : PageModel
    {
        private readonly ICategoryService _service;

        public ListModel(ICategoryService service)
        {
            this._service = service;
        }

        public ICollection<CategoryConciseViewModel> Categories { get; set; }

        public void OnGet()
        {
            this.Categories = this._service.GetCategories().ToList();
        }

        public IActionResult OnPostDelete(int? id)
        {
            if (id == null) return NotFound();

            var category = this._service.GetCategory(id.Value);

            if (category == null) return NotFound();

            this._service.DeleteCategory(category);

            this.TempData.Put(MessageConstants.Name, new MessageModel()
            {
                Type = MessageType.Danger,
                Message = MessageConstants.CategoryDeleteSuccess
            });

            return RedirectToPage(RedirectURL.ToCategoryList, new { Area = NamesConstants.AdminArea });
        }
    }
}