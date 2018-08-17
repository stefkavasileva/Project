using System.Linq;
using Landmarks.Common.Models.Admin.BindingModels;
using Landmarks.Common.Models.Admin.ViewModels;
using Landmarks.Models;

namespace Landmarks.Interfaces.Admin
{
    public interface IRegionService
    {
        void CreateRegion(AddEditRegionBindingModel addRegionBindingModel);

        IQueryable<RegionConciseViewModel> GetRegions();

        Region GetRegion(int id);

        void DeleteRegion(Region region);

        void SaveEntity(AddEditRegionBindingModel model);
    }
}
