using Landmarks.Common.Models.Main.ViewModels;

namespace Landmarks.Interfaces.Main
{
    public interface IRegionService
    {
        RegionDetailsViewModel GetRegionById(int id);
    }
}
