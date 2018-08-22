using System.Linq;
using System.Threading.Tasks;
using Landmarks.Common.Models.Admin.ViewModels;
using Landmarks.Models;

namespace Landmarks.Interfaces.Admin
{
    public interface ICategoryService
    {
        void CreateCategory(string name);

        void DeleteCategory(Category category);

        void SaveEntity(Category category);

        IQueryable<CategoryConciseViewModel> GetCategories();

        Task<Category> GetCategoryAsync(int id);

        Category GetCategory(int id);

        Category GetCategoryByName(string name);
    }
}
