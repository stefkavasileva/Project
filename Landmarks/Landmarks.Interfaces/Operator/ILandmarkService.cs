using System.Collections.Generic;
using Landmarks.Common.Models.Operator.ViewModels;
using Landmarks.Common.Models.Operator.BindingModels;
using Landmarks.Models;
using System.Linq;

namespace Landmarks.Interfaces.Operator
{
    public interface ILandmarkService
    {
        void FillDropDownItems(AddEditLandmarkBindingModel model);

        void AddLandmark(AddEditLandmarkBindingModel addLandmarkBindingModel, List<string> imagesPaths);

        IQueryable<LandmarkConciseViewModel> GetLandmarksByCreatorId(string id);

        Landmark GetLandmark(int id);

        void DeleteLandmark(Landmark landmark);

        void SaveEntity(AddEditLandmarkBindingModel editLandmarkBindingModel, List<string> imagesPaths);
    }
}
