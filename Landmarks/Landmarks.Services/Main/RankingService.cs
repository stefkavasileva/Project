using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Landmarks.Common.Models.Main.ViewModels;
using Landmarks.Data;
using Landmarks.Interfaces.Main;
using Microsoft.EntityFrameworkCore;

namespace Landmarks.Services.Main
{
    public class RankingService : BaseService, IRankingService
    {
        public RankingService(LandmarksDbContext dbContext, IMapper mapper)
            : base(dbContext, mapper)
        {
        }

        public IQueryable<LandmarkRankingViewModel> GetLandmarksByRatingCount()
        {
            var dbLandmarks = this.DbContext.Landmarks
                .Include(l => l.Region)
                .Include(l => l.Category)
                .Include(l => l.DesiredPlaces)
                .Include(l => l.Visitors)
                .OrderByDescending(l => l.Rating)
                .Take(10);

            var modelCollection = this.Mapper.Map<ICollection<LandmarkRankingViewModel>>(dbLandmarks).AsQueryable();

            return modelCollection;
        }

        public IQueryable<LandmarkRankingViewModel> GetLandmarksByComments()
        {
            var dbLandmarks = this.DbContext.Landmarks
                .Include(l => l.Region)
                .Include(l => l.Category)
                .Include(l => l.DesiredPlaces)
                .Include(l => l.Visitors)
                .Include(l=>l.Comments)
                .ThenInclude(c=>c.SubComments)
                .OrderByDescending(l => l.Comments.Sum(c=>c.SubComments.Count))
                .Take(10);

            var modelCollection = this.Mapper.Map<ICollection<LandmarkRankingViewModel>>(dbLandmarks).AsQueryable();

            return modelCollection;
        }

        public IQueryable<LandmarkRankingViewModel> GetLandmarksByVisitation()
        {
            var dbLandmarks = this.DbContext.Landmarks
                .Include(l => l.Region)
                .Include(l => l.Category)
                .Include(l => l.DesiredPlaces)
                .Include(l => l.Visitors)
                .OrderByDescending(l => l.Visitors.Count)
                .Take(10);

            var modelCollection = this.Mapper.Map<ICollection<LandmarkRankingViewModel>>(dbLandmarks).AsQueryable();

            return modelCollection;
        }

        public IQueryable<LandmarkRankingViewModel> GetLandmarksByDesiredVisitation()
        {
            var dbLandmarks = this.DbContext.Landmarks
                .Include(l => l.Region)
                .Include(l => l.Category)
                .Include(l => l.DesiredPlaces)
                .Include(l => l.Visitors)
                .OrderByDescending(l => l.DesiredPlaces.Count)
                .Take(10);

            var modelCollection = this.Mapper.Map<ICollection<LandmarkRankingViewModel>>(dbLandmarks).AsQueryable();

            return modelCollection;
        }
    }
}
