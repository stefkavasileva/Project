using Landmarks.Common.Models.Main.ViewModel;
using Landmarks.Models;
using System.Linq;

namespace Landmarks.Interfaces.Main
{
    public interface ILandmarkService
    {
        LandmarkDetailsViewModel GetLandmarkById(int id);

        void SaveRate(Landmark landmark);

        Landmark GetLandmarkFromDbById(int id);

        IQueryable<LandmarkViewModel> GetAllLandmark();
    }
}

