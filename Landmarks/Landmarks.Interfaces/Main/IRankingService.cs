using System.Linq;
using Landmarks.Common.Models.Main.ViewModel;

namespace Landmarks.Interfaces.Main
{
    public interface IRankingService
    {
        IQueryable<LandmarkRankingViewModel> GetLandmarksByRatingCount();

        IQueryable<LandmarkRankingViewModel> GetLandmarksByComments();

        IQueryable<LandmarkRankingViewModel> GetLandmarksByVisitation();

        IQueryable<LandmarkRankingViewModel> GetLandmarksByDesiredVisitation();
    }   
}
