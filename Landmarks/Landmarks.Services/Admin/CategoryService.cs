using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Landmarks.Common.Models.Admin.ViewModels;
using Landmarks.Data;
using Landmarks.Models;
using Landmarks.Interfaces.Admin;
using Microsoft.EntityFrameworkCore;

namespace Landmarks.Services.Admin
{
    public class CategoryService : BaseService, ICategoryService
    {
        public CategoryService(LandmarksDbContext dbContext, IMapper mapper)
            : base(dbContext, mapper)
        {
        }

        public IEnumerable<CategoryConciseViewModel> GetCategories()
        {
            var dbCategories = this.DbContext.Categories.ToList();
            var modelCategories = this.Mapper.Map<ICollection<CategoryConciseViewModel>>(dbCategories);

            return modelCategories;
        }

      
        public void CreateCategory(string name)
        {
            var category = new Category { Name = name };
            this.DbContext.Categories.Add(category);
            this.DbContext.SaveChangesAsync();
        }

        public async Task<Category> GetCategoryAsync(int id)
        {
            return await this.DbContext.Categories.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
        }

        public void DeleteCategoryAsync(Category category)
        {
            this.DbContext.Remove(category);
            this.DbContext.SaveChangesAsync();
        }

        public Category GetCategoryByName(string name)
        {
            return this.DbContext.Categories.FirstOrDefault(c => c.Name == name);
        }

        public void SaveEntity(Category category)
        {
            this.DbContext.Attach(category).State = EntityState.Modified;
            this.DbContext.SaveChanges();
        }
    }
}
