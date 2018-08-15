using Landmarks.Common.Models.Admin.BindingModels;
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
    public class AddModel : PageModel
    {
        private readonly ICategoryService _service;

        public AddModel(ICategoryService service)
        {
            this._service = service;
        }

        [BindProperty]
        public AddEditCategoryBindingModel AddCategoryBindingModel { get; set; }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                this._service.CreateCategory(this.AddCategoryBindingModel.Name);

                PushNotificationMessage();

                return RedirectToPage(RedirectURL.ToCategoryList, new { Area = NamesConstants.AdminArea });
            }

            return Page();
        }

        private void PushNotificationMessage()
        {
            this.TempData.Put(MessageConstants.Name, new MessageModel()
            {
                Type = MessageType.Success,
                Message = MessageConstants.CategorySuccess
            });
        }
    }
}