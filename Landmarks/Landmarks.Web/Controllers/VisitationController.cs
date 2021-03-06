﻿using Landmarks.Interfaces.Main;
using Landmarks.Web.Common.Constants;
using Landmarks.Web.Common.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Landmarks.Web.Controllers
{
    public class VisitationController : Controller
    {
        private readonly IVisitationService _service;

        public VisitationController(IVisitationService service)
        {
            this._service = service;
        }

        public IActionResult MakeVisited(int? landmarkId)
        {
            var userId = this.User.GetUserId();
            if (userId == null)
            {
                return Json(new { redirectUrl = RedirectURL.ToLoginPage });
            }

            if (landmarkId == null) return NotFound();
            var landmark = this._service.GetLatmarkById(landmarkId.Value);

            if (landmark == null) return NotFound();

            this._service.SaveEntity(landmark.Id, userId);

            return Json(new { success = true });
        }

        public IActionResult RemoveFromVisited(int? landmarkId)
        {
            var userId = this.User.GetUserId();
            if (userId == null)
            {
                return Json(new { redirectUrl = RedirectURL.ToLoginPage });
            }

            if (landmarkId == null) return NotFound();
            var landmark = this._service.GetLatmarkById(landmarkId.Value);

            if (landmark == null) return NotFound();

            var entity = _service.GetCurrentVisitedPlace(landmark.Id, userId);

            this._service.RemoveEntity(entity);

            return Json(new { success = true });
        }

        public IActionResult IsVisited(int? landmarkId)
        {
            var userId = this.User.GetUserId();
            if (userId == null)
            {
                return Json(new { isVisited = false });
            }

            if (landmarkId == null) return NotFound();

            var isVisited = _service.IsVisitedByUser(landmarkId.Value, userId);

            return Json(new { isVisited });

        }

        public IActionResult MakeDesiredVisited(int? landmarkId)
        {
            var userId = this.User.GetUserId();
            if (userId == null)
            {
                return Json(new { redirectUrl = RedirectURL.ToLoginPage });
            }

            if (landmarkId == null) return NotFound();
            var landmark = this._service.GetLatmarkById(landmarkId.Value);

            if (landmark == null) return NotFound();

            this._service.SaveDesiredEntity(landmark.Id, userId);

            return Json(new { success = true });
        }

        public IActionResult RemoveFromDesiredVisitation(int? landmarkId)
        {
            var userId = this.User.GetUserId();
            if (userId == null)
            {
                return Json(new { redirectUrl = RedirectURL.ToLoginPage });
            }

            if (landmarkId == null) return NotFound();
            var landmark = this._service.GetLatmarkById(landmarkId.Value);

            if (landmark == null) return NotFound();

            var entity = _service.GetCurrentDesiredPlace(landmark.Id, userId);

            this._service.RemoveEntity(entity);

            return Json(new { success = true });
        }

        public IActionResult IsDesiredPlaceVisited(int? landmarkId)
        {
            var userId = this.User.GetUserId();
            if (userId == null)
            {
                return Json(new { isVisited = false });
            }

            if (landmarkId == null) return NotFound();

            var isVisited = _service.IsDesiredPlaceVisitedByUser(landmarkId.Value, userId);

            return Json(new { isVisited });

        }
    }
}