using System.Collections.Generic;
using Landmarks.Common.Models.Admin.BindingModels;
using Landmarks.Common.Models.Admin.ViewModels;
using Landmarks.Models;

namespace Landmarks.Interfaces.Admin
{
    public interface IRegionService
    {
         void CreateRegion(AddEditRegionBindingModel addRegionBindingModel);

        IEnumerable<RegionConciseViewModel> GetRegions();

        Region GetRegion(int id);

        void DeleteRegion(Region region);

        void SaveEntity(AddEditRegionBindingModel model);

        void FillDropDownItems(AddEditRegionBindingModel model);
    }
}
