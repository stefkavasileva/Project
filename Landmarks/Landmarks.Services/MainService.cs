using AutoMapper;
using Landmarks.Data;
using Landmarks.Interfaces;
using Landmarks.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Landmarks.Services
{
    public class MainService : BaseService, IMainService
    {
        public MainService(LandmarksDbContext dbContext, IMapper mapper)
            : base(dbContext, mapper)
        {
        }

        public Region GetRegionInfoByName(string regionName)
        {
            return this.DbContext.Regions.Include(r => r.Landmarks).FirstOrDefault(r => r.Name == regionName);
        }
    }
}
