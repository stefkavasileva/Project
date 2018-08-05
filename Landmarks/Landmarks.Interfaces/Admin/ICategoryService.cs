using System.Collections.Generic;
using System.Threading.Tasks;
using Landmarks.Common.Models.Admin.ViewModels;
using Landmarks.Models;

namespace Landmarks.Interfaces.Admin
{
    public interface ICategoryService
    {
        IEnumerable<CategoryConciseViewModel> GetCategories();

        void CreateCategory(string name);

        Task<Category> GetCategoryAsync(int id);

        void DeleteCategoryAsync(Category category);

        Category GetCategoryByName(string name);

        void SaveEntity(Category category);
    }
}
