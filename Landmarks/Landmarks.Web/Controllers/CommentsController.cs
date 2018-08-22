using System.Linq;
using Landmarks.Common.Models.Main.ViewModels;
using Landmarks.Interfaces.Main;
using Landmarks.Web.Common.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Landmarks.Web.Controllers
{
    public class CommentsController : Controller
    {
        private readonly ICommentsService _service;

        public CommentsController(ICommentsService service)
        {
            this._service = service;
        }

        public ActionResult GetUsers()
        {
            return View();
        }

        public ActionResult GetUsers(string email)
        {
            var user = this._service.GetUserByEmail(email);

            return View(user);
        }

        public PartialViewResult GetComments(int landmarkId)
        {
            var comments = this._service.GetLandmarksComment(landmarkId);

            return PartialView("~/Views/Shared/_MyComments.cshtml", comments);
        }

        [HttpPost]
        public ActionResult AddComment(CommentsViewModel comment, int landmarkId)
        {
            var userId = this.User.GetUserId();
            this._service.SaveComment(userId, landmarkId, comment);

            return RedirectToAction("GetComments", "Comments", new { landmarkId = landmarkId });
        }

        public PartialViewResult GetSubComments(int commentId)
        {
            var subComments = this._service.GetSubComments(commentId);

            return PartialView("~/Views/Shared/_MySubComments.cshtml", subComments);
        }

        [HttpPost]
        public ActionResult AddSubComment(SubCommentsViewModel subComment, int commentId)
        {
            var userId = this.User.GetUserId();
            this._service.SaveSubComment(userId, subComment, commentId);

            return RedirectToAction("GetSubComments", "Comments", new { commentId = commentId });

        }
    }
}