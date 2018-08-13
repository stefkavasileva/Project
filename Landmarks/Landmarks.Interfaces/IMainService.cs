using Landmarks.Models;

namespace Landmarks.Interfaces
{
    public interface IMainService
    {
        Region GetRegionInfoByName(string regionName);
    }
}
