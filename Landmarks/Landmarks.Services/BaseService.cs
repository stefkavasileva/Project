using AutoMapper;
using Landmarks.Data;

namespace Landmarks.Services
{
    public abstract class BaseService
    {
        protected BaseService(LandmarksDbContext dbContext,IMapper mapper)
        {
            this.DbContext = dbContext; 
            this.Mapper = mapper;
        }

        protected LandmarksDbContext DbContext { get; set; }

        protected IMapper Mapper { get; set; }
    }
}
