using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using AutoMapper;
using Landmarks.Common.Models.Admin.BindingModels;
using Landmarks.Common.Models.Admin.ViewModels;
using Landmarks.Data;
using Landmarks.Interfaces.Admin;
using Landmarks.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace Landmarks.Services.Admin
{
    public class RegionService : BaseService, IRegionService
    {
        public static readonly List<string> RegionNames = new List<string> { "Veliko Tarnovo", "Montana", "Vratsa", "Kyustendil", "Vidin", "Burgas", "Yambol", "Targovishte", "Razgrad", "Shumen", "Dobrich", "Varna", "Silistra", "Ruse", "Blagoevgrad", "Sliven", "Stara Zagora", "Haskovo", "Plovdiv", "Pazardzhik", "Smolyan", "Kardzhali", "Sofia", "Grad Sofiya", "Pernik", "Gabrovo", "Lovech", "Pleven", };

        private readonly IHostingEnvironment _environment;

        public RegionService(LandmarksDbContext dbContext, IMapper mapper, IHostingEnvironment environment)
            : base(dbContext, mapper)
        {
            this._environment = environment;
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
            RemoveImagesFromServer(region);

            this.DbContext.Remove(region);
            this.DbContext.SaveChanges();
        }

        public void SaveEntity(AddEditRegionBindingModel model)
        {
            var region = this.Mapper.Map<Landmarks.Models.Region>(model);

            this.DbContext.Attach(region).State = EntityState.Modified;
            this.DbContext.SaveChanges();
        }

        private void RemoveImagesFromServer(Region region)
        {
            //before delete region, remove all landmark images saved on the server
            var landmarksImages = this.DbContext
                .Landmarks
                .Where(l => l.RegionId == region.Id)
                .Select(l => l.Images);

            var ladnamrkImagesPath = new List<string>();
            foreach (var images in landmarksImages)
            {
                foreach (var image in images)
                {
                    ladnamrkImagesPath.Add(image.Path);
                }
            }

            foreach (var path in ladnamrkImagesPath)
            {
                var newPath = Regex.Replace(path, "/", "\\").Substring(1);

                var fullFilePath = Path.Combine(_environment.WebRootPath + newPath);

                if (File.Exists(fullFilePath))
                {
                    File.Delete(fullFilePath);
                }
            }
        }
    }
}
