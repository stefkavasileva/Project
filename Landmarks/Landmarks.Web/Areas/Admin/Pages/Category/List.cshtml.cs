using Landmarks.Common.Models.Admin.ViewModels;
using Landmarks.Interfaces.Admin;
using Landmarks.Web.Common.Constants;
using Landmarks.Web.Common.Extensions;
using Landmarks.Web.Common.Helpers;
using Landmarks.Web.Common.Helpers.Messages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace Landmarks.Web.Areas.Admin.Pages.Category
{
    [Authorize(Roles = NamesConstants.RoleAdmin)]
    public class ListModel : PageModel
    {
        private const int DefaultPageSize = 5;
        private readonly ICategoryService _service;

        public ListModel(ICategoryService service)
        {
            this._service = service;
        }

        public PaginatedList<CategoryConciseViewModel> Categories { get; set; }

        public void OnGet(int? pageIndex)
        {
            var viewModel = this._service.GetCategories();

            int pageSize = DefaultPageSize;
            this.Categories = PaginatedList<CategoryConciseViewModel>.Create(viewModel, pageIndex ?? 1, pageSize);
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