using System.Linq;
using AutoMapper;
using Landmarks.Data;
using Landmarks.Interfaces.Main;
using Landmarks.Models;

namespace Landmarks.Services.Main
{
    public class VisitationService : BaseService, IVisitationService
    {
        public VisitationService(LandmarksDbContext dbContext, IMapper mapper)
            : base(dbContext, mapper)
        {
        }


        public void SaveEntity(int landmarkId, string userId)
        {
           var result = new VisitedPlaces { LandmarkId = landmarkId,UserId = userId};

            this.DbContext.VisitedPlaces.Add(result);
            this.DbContext.SaveChanges();
        }

        public Landmark GetLatmarkById(int landmarkId)
        {
            return this.DbContext.Landmarks.Find(landmarkId);
        }

        public VisitedPlaces GetCurrentVisitedPlace(int landmarkId, string userId)
        {
            var entity = this.DbContext.VisitedPlaces.FirstOrDefault(e => e.LandmarkId == landmarkId && e.UserId == userId);
            return entity;
        }

        public void RemoveEntity(VisitedPlaces place)
        {
            this.DbContext.VisitedPlaces.Remove(place);
            this.DbContext.SaveChanges();
        }

        public bool IsVisitedByUser(int landmarkId, string userId)
        {
            var entity = this.GetCurrentVisitedPlace(landmarkId, userId);

            return entity != null;
        }
    }
}
