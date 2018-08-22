using System.Linq;
using Landmarks.Common.Models.Admin.BindingModels;
using Landmarks.Common.Models.Admin.ViewModels;
using Landmarks.Models;

namespace Landmarks.Interfaces.Admin
{
    public interface ILandmarkService
    {
        IQueryable<LandmarkConciseViewModel> GetLandmarks();

        Landmark GetLandmark(int id);

        void FillDropDownItems(AddEditLandmarkBindingModel model);

        void CreateLandmark(AddEditLandmarkBindingModel addRegionBindingModel);

        void SaveEntity(AddEditLandmarkBindingModel editLandmarkBindingModel);

        void DeleteLandmark(Landmark landmark);
    }
}
