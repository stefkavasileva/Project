using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Landmarks.Common.Models.Main.ViewModels;
using Landmarks.Data;
using Landmarks.Interfaces.Main;
using Microsoft.EntityFrameworkCore;

namespace Landmarks.Services.Main
{
    public class CategoryService : BaseService, ICategoryService
    {
        public CategoryService(LandmarksDbContext dbContext, IMapper mapper) 
            : base(dbContext, mapper)
        {
        }

        public IQueryable<ListCategoriesViewModel> GetCategories()
        {
            var dbCategories = this.DbContext.Categories.Include(c=>c.Landmarks)
                .Select(e=> new ListCategoriesViewModel
                {
                    Id = e.Id,
                    Name = e.Name,
                    LandmarksCount = e.Landmarks.Count,
                })
                .OrderByDescending(e=>e.LandmarksCount);

            var modelCategories = this.Mapper.Map<ICollection<ListCategoriesViewModel>>(dbCategories).AsQueryable();

            return modelCategories;
        }

        public LandmarksByCategoryViewModel GetLandmarksByCategory(int categoryIdValue)
        {
            var queryResult = from land in this.DbContext.Landmarks
                              join category in this.DbContext.Categories on land.CategoryId equals category.Id into categoryLandsJoin
                              from categoryLands in categoryLandsJoin.DefaultIfEmpty()
                              join user in this.DbContext.Users on land.CreatorId equals user.Id
                              where categoryLands.Id == categoryIdValue
                              select new LandmarksByCategoryViewModel
                              {                               
                                  Name = categoryLands.Name,
                                  Landmarks = categoryLands
                                                .Landmarks
                                                .Select(l => new LandmarkConciseViewModel
                                                {
                                                    CategoryName = l.Category.Name,
                                                    Name = l.Name,
                                                    LandmarkId = l.Id,
                                                    Description = l.Description,
                                                    RegionName = l.Region.Name,
                                                    CreatedDate = l.PostedDate,
                                                    CreatorEmail = user.Email,
                                                    MainPagePath = l.Images.FirstOrDefault() == null ? "" : l.Images.First().Path
                                                }).ToList(),
                              };

            var viewModel = queryResult.FirstOrDefault();

            //if category does not have landmarks, get main information about it
            if (viewModel is null)
            {
                var categoryDb = this.DbContext.Categories.Find(categoryIdValue);
                viewModel = this.Mapper.Map<LandmarksByCategoryViewModel>(categoryDb);
            }

            return viewModel;
        }
    }
}
