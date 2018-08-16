using AutoMapper;
using Landmarks.Common.Models.Main.ViewModel;
using Landmarks.Data;
using Landmarks.Interfaces.Main;
using System.Linq;


namespace Landmarks.Services.Main
{
    public class RegionService : BaseService, IRegionService
    {
        public RegionService(LandmarksDbContext dbContext, IMapper mapper)
             : base(dbContext, mapper)
        {
        }

        public RegionDetailsViewModel GetRegionById(int id)
        {
            var queryResult = from land in this.DbContext.Landmarks
                              join region in this.DbContext.Regions on land.RegionId equals region.Id into regionLandsJoin
                              from regionLands in regionLandsJoin.DefaultIfEmpty()
                              join category in this.DbContext.Categories on land.CategoryId equals category.Id into categoryLandsJoin
                              from categoryLands in categoryLandsJoin.DefaultIfEmpty()
                              where regionLands.Id == id
                              select new RegionDetailsViewModel
                              {
                                  Area = regionLands.Area,
                                  Id = regionLands.Id,
                                  Name = regionLands.Name,
                                  Population = regionLands.Population,
                                  Landmarks = regionLands
                                                .Landmarks
                                                .Select(l => new LandmarkConciseViewModel
                                                {
                                                    CategoryName = l.Category.Name,
                                                    Name = l.Name,
                                                    LandmarkId = l.Id,
                                                    Description = l.Description,
                                                    RegionName = l.Region.Name,
                                                    MainPagePath = l.Images.FirstOrDefault() == null ? "" : l.Images.First().Path
                                                }).ToList(),
                              };

            var viewModel = queryResult.FirstOrDefault();

            return viewModel;
        }
    }
}
