using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Landmarks.Common.Models.Admin.BindingModels;
using Landmarks.Common.Models.Admin.ViewModels;
using Landmarks.Data;
using Landmarks.Interfaces.Admin;
using Microsoft.AspNetCore.Mvc.Rendering;
using Landmarks.Models;
using Microsoft.EntityFrameworkCore;

namespace Landmarks.Services.Admin
{
    public class LandmarkService : BaseService, ILandmarkService
    {
        public LandmarkService(LandmarksDbContext dbContext, IMapper mapper)
            : base(dbContext, mapper)
        {
        }

        public void FillDropDownItems(AddEditLandmarkBindingModel model)
        {
            //get categories and region for dropdown list
            var categories = this.DbContext.Categories.ToList();
            var regions = this.DbContext.Regions.ToList();

            model.Categories = new List<SelectListItem>();
            categories.ForEach(c => model.Categories.Add(new SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString()
            }));

            model.Regions = new List<SelectListItem>();
            regions.ForEach(r => model.Regions.Add(new SelectListItem
            {
                Text = r.Name,
                Value = r.Id.ToString()
            }));
        }

        public void CreateLandmark(AddEditLandmarkBindingModel addRegionBindingModel)
        {
            var landmark = this.Mapper.Map<AddEditLandmarkBindingModel, Landmark>(addRegionBindingModel);
            this.DbContext.Landmarks.Add(landmark);
            this.DbContext.SaveChanges();
        }

        public IQueryable<LandmarkConciseViewModel> GetLandmarks()
        {
            var dbLandmarks = this.DbContext.Landmarks.Include(l => l.Region).Include(l => l.Category);
            var modelCollection = this.Mapper.Map<ICollection<LandmarkConciseViewModel>>(dbLandmarks).AsQueryable();

            return modelCollection;
        }

        public Landmark GetLandmark(int id)
        {
            return this.DbContext.Landmarks.FirstOrDefault(l => l.Id == id);
        }

        public void SaveEntity(AddEditLandmarkBindingModel editLandmarkBindingModel)
        {
            var landmark = this.Mapper.Map<Landmarks.Models.Landmark>(editLandmarkBindingModel);

            this.DbContext.Attach(landmark).State = EntityState.Modified;
            this.DbContext.SaveChanges();
        }

        public void DeleteLandmark(Landmark landmark)
        {
            this.DbContext.Remove(landmark);
            this.DbContext.SaveChanges();
        }
    }
}
