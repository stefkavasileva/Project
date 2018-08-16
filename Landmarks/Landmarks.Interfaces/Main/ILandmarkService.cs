using Landmarks.Common.Models.Main.ViewModel;
using Landmarks.Models;

namespace Landmarks.Interfaces.Main
{
    public interface ILandmarkService
    {
        LandmarkDetailsViewModel GetLandmarkById(int id);

        void SaveRate(Landmark landmark);
            
        Landmark GetLandmarkFromDbById(int id);
    }
}

