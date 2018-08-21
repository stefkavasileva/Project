using System.Linq;
using Landmarks.Interfaces.Main;
using Landmarks.Web.Common.Constants;
using Microsoft.AspNetCore.Mvc;

namespace Landmarks.Web.Controllers
{
    public class RankingController : Controller
    {
        private readonly IRankingService _service;

        public RankingController(IRankingService service)
        {
            this._service = service;
        }

        public IActionResult TopTenByRating()
        {
            var viewModel = this._service.GetLandmarksByRatingCount().ToList();
            
            return View(viewModel);
        }

        public IActionResult TopTenByComments()
        {
            var viewModel = this._service.GetLandmarksByComments().ToList();

            return View(NamesConstants.RankingViewName, viewModel);
        }

        public IActionResult TopTenByVisitation()
        {
            var viewModel = this._service.GetLandmarksByVisitation().ToList();

            return View(NamesConstants.RankingViewName,viewModel);
        }

        public IActionResult TopTenByDesiredVisitation()
        {
            var viewModel = this._service.GetLandmarksByDesiredVisitation().ToList();

            return View(NamesConstants.RankingViewName,viewModel);
        }
    }
}