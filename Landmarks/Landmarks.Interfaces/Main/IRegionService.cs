using Landmarks.Common.Models.Main.ViewModel;

namespace Landmarks.Interfaces.Main
{
    public interface IRegionService
    {
        RegionDetailsViewModel GetRegionById(int id);
    }
}
