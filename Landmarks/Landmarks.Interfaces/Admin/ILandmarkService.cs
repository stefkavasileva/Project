using System.Linq;
using Landmarks.Common.Models.Admin.BindingModels;
using Landmarks.Common.Models.Admin.ViewModels;
using Landmarks.Models;

namespace Landmarks.Interfaces.Admin
{
    public interface ILandmarkService
    {
        void FillDropDownItems(AddEditLandmarkBindingModel model);

        void CreateLandmark(AddEditLandmarkBindingModel addRegionBindingModel);

        IQueryable<LandmarkConciseViewModel> GetLandmarks();

        Landmark GetLandmark(int id);

        void SaveEntity(AddEditLandmarkBindingModel editLandmarkBindingModel);

        void DeleteLandmark(Landmark landmark);
    }
}
