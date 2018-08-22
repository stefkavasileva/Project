using AutoMapper;
using Landmarks.Common.Models.Main.ViewModels;
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
                              join region in this.DbContext.Regions on land.RegionId equals region.Id into landmarkRegionJoin
                              from landmarkRegion in landmarkRegionJoin.DefaultIfEmpty()
                              join category in this.DbContext.Categories on land.CategoryId equals category.Id into categoryLandsJoin
                              from categoryLands in categoryLandsJoin.DefaultIfEmpty()
                              join user in this.DbContext.Users on land.CreatorId equals user.Id
                              where landmarkRegion.Id == id
                              select new RegionDetailsViewModel
                              {
                                  Area = landmarkRegion.Area,
                                  Id = landmarkRegion.Id,
                                  Name = landmarkRegion.Name,
                                  Population = landmarkRegion.Population,
                                  Landmarks = landmarkRegion
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

            //if region does not have landmarks, get main information about it
            if (viewModel is null)
            {
                var regionDb = this.DbContext.Regions.Find(id);
                viewModel = this.Mapper.Map<RegionDetailsViewModel>(regionDb);
            }

            return viewModel;
        }
    }
}

