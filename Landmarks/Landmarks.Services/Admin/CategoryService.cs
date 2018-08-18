using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AutoMapper;
using Landmarks.Common.Models.Admin.ViewModels;
using Landmarks.Data;
using Landmarks.Models;
using Landmarks.Interfaces.Admin;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace Landmarks.Services.Admin
{
    public class CategoryService : BaseService, ICategoryService
    {
        private readonly IHostingEnvironment _environment;

        public CategoryService(LandmarksDbContext dbContext, IMapper mapper, IHostingEnvironment environment)
            : base(dbContext, mapper)
        {
            this._environment = environment;
        }

        public IQueryable<CategoryConciseViewModel> GetCategories()
        {
            var dbCategories = this.DbContext.Categories;
            var modelCategories = this.Mapper.Map<ICollection<CategoryConciseViewModel>>(dbCategories).AsQueryable();

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

        public Category GetCategory(int id)
        {
            return this.DbContext.Categories.AsNoTracking().FirstOrDefault(c => c.Id == id);
        }

        public void DeleteCategory(Category category)
        {
            RemoveImagesFromServer(category);

            this.DbContext.Remove(category);
            this.DbContext.SaveChanges();
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

        private void RemoveImagesFromServer(Category category)
        {
            //before delete category, remove all landmark images saved on the server
            var landmarksImages = this.DbContext
                .Landmarks
                .Where(l => l.CategoryId == category.Id)
                .Select(l => l.Images);

            var ladnamrkImagesPath = new List<string>();
            foreach (var images in landmarksImages)
            {
                foreach (var image in images)
                {
                    ladnamrkImagesPath.Add(image.Path);
                }
            }

            foreach (var path in ladnamrkImagesPath)
            {
                var newPath = Regex.Replace(path, "/", "\\").Substring(1);

                var fullFilePath = Path.Combine(_environment.WebRootPath + newPath);

                if (File.Exists(fullFilePath))
                {
                    File.Delete(fullFilePath);
                }
            }
        }

    }
}
