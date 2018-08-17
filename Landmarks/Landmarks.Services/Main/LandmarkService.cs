using AutoMapper;
using Landmarks.Common.Models.Main.ViewModel;
using Landmarks.Data;
using Landmarks.Interfaces.Main;
using System.Linq;
using Landmarks.Models;
using Microsoft.EntityFrameworkCore;

namespace Landmarks.Services.Main
{
    public class LandmarkService : BaseService, ILandmarkService
    {
        public LandmarkService(LandmarksDbContext dbContext, IMapper mapper)
            : base(dbContext, mapper)
        {
        }

        public LandmarkDetailsViewModel GetLandmarkById(int id)
        {
            var queryResult = from landmark in this.DbContext.Landmarks
                              join reg in this.DbContext.Regions on landmark.RegionId equals reg.Id into regionLandsJoin
                              from regionLands in regionLandsJoin.DefaultIfEmpty()
                              join category in this.DbContext.Categories on landmark.CategoryId equals category.Id into categoryLandsJoin
                              from categoryLands in categoryLandsJoin.DefaultIfEmpty()
                              where landmark.Id == id
                              select new LandmarkDetailsViewModel
                              {
                                  Id = landmark.Id,
                                  UserIdsRatedPic = string.IsNullOrWhiteSpace(landmark.UserIdsRatedPic) ? string.Empty : landmark.UserIdsRatedPic,
                                  Rating = landmark.Rating,
                                  CategoryName = categoryLands.Name,
                                  Name = landmark.Name,
                                  Description = landmark.Description,
                                  ImagesPath = landmark.Images.Select(i => i.Path).ToList(),
                                  RegionArea = regionLands.Area,
                                  RegionName = regionLands.Name,
                                  RegionPopulation = regionLands.Population
                              };

            var viewModel = queryResult.FirstOrDefault();

            return viewModel;
        }

        public void SaveRate(Landmark landmark)
        {
            this.DbContext.Landmarks.Attach(landmark).State = EntityState.Modified;
            this.DbContext.SaveChanges();
        }

        public Landmark GetLandmarkFromDbById(int id)
        {
            return this.DbContext.Landmarks.FirstOrDefault(p => p.Id == id);
        }

        public IQueryable<LandmarkViewModel> GetAllLandmark()
        {
            var result = this.DbContext.Landmarks
                                         .Select(l => new LandmarkViewModel
                                         {
                                             Id = l.Id,
                                             Name = l.Name,
                                             Description = l.Description,
                                             PostedDate = l.PostedDate
                                         }).AsQueryable();

            return result;
        }
    }
}
