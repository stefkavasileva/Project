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
        public static readonly List<string> RegionNames = new List<string> { "Veliko Tarnovo", "Montana", "Vratsa", "Kyustendil", "Vidin", "Burgas", "Yambol", "Targovishte", "Razgrad", "Shumen", "Dobrich", "Varna", "Silistra", "Ruse", "Blagoevgrad", "Sliven", "Stara Zagora", "Haskovo", "Plovdiv", "Pazardzhik", "Smolyan", "Kardzhali", "Sofia", "Grad Sofiya", "Pernik", "Gabrovo", "Lovech", "Pleven", };

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

        public IQueryable<RegionConciseViewModel> GetRegions()
        {
            var dbRegions = this.DbContext.Regions;
            var modelRegions = this.Mapper.Map<ICollection<RegionConciseViewModel>>(dbRegions).AsQueryable();

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
