using System.Linq;
using Landmarks.Common.Models.Main.ViewModels;

namespace Landmarks.Interfaces.Main
{
    public interface ICategoryService
    {
        IQueryable<ListCategoriesViewModel> GetCategories();

        LandmarksByCategoryViewModel GetLandmarksByCategory(int categoryIdValue);
    }   
}   
