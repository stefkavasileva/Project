using System.Linq;
using Landmarks.Common.Models.Admin.BindingModels;
using Landmarks.Common.Models.Admin.ViewModels;
using Landmarks.Models;

namespace Landmarks.Interfaces.Admin
{
    public interface IRegionService
    {
        IQueryable<RegionConciseViewModel> GetRegions();

        Region GetRegion(int id);

        void CreateRegion(AddEditRegionBindingModel addRegionBindingModel);

        void DeleteRegion(Region region);

        void SaveEntity(AddEditRegionBindingModel model);
    }
}
