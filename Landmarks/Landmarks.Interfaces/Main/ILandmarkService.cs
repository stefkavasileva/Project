using Landmarks.Common.Models.Main.ViewModels;
using Landmarks.Models;
using System.Linq;

namespace Landmarks.Interfaces.Main
{
    public interface ILandmarkService
    {
        void SaveRate(Landmark landmark);

        LandmarkDetailsViewModel GetLandmarkById(int id);

        Landmark GetLandmarkFromDbById(int id);

        IQueryable<LandmarkViewModel> GetAllLandmark();
    }
}

