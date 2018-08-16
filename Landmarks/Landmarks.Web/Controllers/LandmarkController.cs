using Landmarks.Interfaces.Main;
using Landmarks.Web.Common.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Landmarks.Web.Controllers
{
    public class LandmarkController : Controller
    {
        private readonly ILandmarkService _service;

        public LandmarkController(ILandmarkService service)
        {
            this._service = service;
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var viewModel = this._service.GetLandmarkById(id);

            return View(viewModel);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult RateLandmark(FormCollection form)
        {
            var rating = int.Parse(form["Rating"]);
            var landmarkId = int.Parse(form["landmarkId"]);

            if (rating != 0)
            {
                var landmark = this._service.GetLandmarkFromDbById(landmarkId);
                landmark.RatingSum += rating;
                landmark.RatingCount++;
                landmark.Rating = landmark.RatingSum / landmark.RatingCount;
                // Getting the ID of the currently logged in user
                var currentUserId = this.User.GetUserId();
                // Adding the ID to the IDs of the users that have already rated this picture
                landmark.UserIdsRatedPic += " " + currentUserId;

                this._service.SaveRate(landmark);             
            }

            return RedirectToAction("Details", new { id = landmarkId });
        }
    }
}