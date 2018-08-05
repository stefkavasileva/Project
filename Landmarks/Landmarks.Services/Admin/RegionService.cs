using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Landmarks.Common.Models.Admin.BindingModels;
using Landmarks.Common.Models.Admin.ViewModels;
using Landmarks.Data;
using Landmarks.Interfaces.Admin;
using Landmarks.Models;
using Microsoft.EntityFrameworkCore;

namespace Landmarks.Services.Admin
{
    public class RegionService : BaseService, IRegionService
    {
        public RegionService(LandmarksDbContext dbContext, IMapper mapper)
            : base(dbContext, mapper)
        {
        }

        public void CreateRegion(AddEditRegionBindingModel regionModel)
        {
            var region = this.Mapper.Map<AddEditRegionBindingModel, Region>(regionModel);

            this.DbContext.Regions.Add(region);
            this.DbContext.SaveChangesAsync();
        }

        public IEnumerable<RegionConciseViewModel> GetRegions()
        {
            var dbRegions = this.DbContext.Regions.ToList();
            var modelRegions = this.Mapper.Map<ICollection<RegionConciseViewModel>>(dbRegions);

            return modelRegions;
        }

        public Region GetRegion(int id)
        {
            return this.DbContext.Regions.FirstOrDefault(x => x.Id == id);
        }

        public void DeleteRegion(Region region)
        {
            this.DbContext.Remove(region);
            this.DbContext.SaveChanges();
        }

        public void SaveEntity(AddEditRegionBindingModel model)
        {
            var region = this.Mapper.Map<Landmarks.Models.Region>(model);

            this.DbContext.Attach(region).State = EntityState.Modified;
            this.DbContext.SaveChanges();
        }
    }
}
