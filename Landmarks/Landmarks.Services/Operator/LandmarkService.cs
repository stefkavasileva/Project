using System.Collections.Generic;
using System.IO;
using System.Linq;
using AutoMapper;
using Landmarks.Common.Models.Operator.BindingModels;
using Landmarks.Common.Models.Operator.ViewModels;
using Landmarks.Data;
using Landmarks.Interfaces.Operator;
using Landmarks.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Landmarks.Services.Operator
{
    public class LandmarkService :BaseService, ILandmarkService
    {
        private readonly IHostingEnvironment _environment;
        public LandmarkService(LandmarksDbContext dbContext, IMapper mapper, IHostingEnvironment environment)
            : base(dbContext, mapper)
        {
            this._environment = environment;
        }

        public void FillDropDownItems(AddEditLandmarkBindingModel model)
        {
            //get categories and region for drop down list
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

        public void AddLandmark(AddEditLandmarkBindingModel model, List<string> imagesPaths)
        {            
            var landmark = new Landmark
            {
                Name = model.Name,               
                CategoryId = model.CategoryId,
                Description = model.Description,
                RegionId = model.RegionId,
            };

            this.DbContext.Landmarks.Add(landmark);
            this.DbContext.SaveChanges();

            if(!imagesPaths.Any()) return;

            var imagesToDb = new List<Image>();
            foreach (var imagesPath in imagesPaths)
            {
                imagesToDb.Add(new Image{LandmarkId = landmark.Id,Path = imagesPath});
            }

            this.DbContext.Image.AddRange(imagesToDb);
            this.DbContext.SaveChanges();

            landmark.Images = imagesToDb;

            this.DbContext.SaveChanges();
        }


        public ICollection<LandmarkConciseViewModel> GetLandmarks()
        {
            var dbLandmarks = this.DbContext.Landmarks.Include(l => l.Region).Include(l => l.Category).ToList();
            var modelCollection = new List<LandmarkConciseViewModel>();

            dbLandmarks.ForEach(l => modelCollection.Add(this.Mapper.Map<Landmark, LandmarkConciseViewModel>(l)));

            return modelCollection;
        }

        public Landmark GetLandmark(int id)
        {
            return this.DbContext.Landmarks.Include(l=>l.Images).FirstOrDefault(l => l.Id == id);
        }

        public void DeleteLandmark(Landmark landmark)
        {
            //remove child
            foreach (var landmarkImage in landmark.Images)
            {
                var image = this.DbContext.Image.FirstOrDefault(i => i.Id == landmarkImage.Id);
                if(image == null) continue;
               
                
                    File.Delete(image.Path);
                

                this.DbContext.Image.Remove(image);
            }

            this.DbContext.Remove(landmark);
            this.DbContext.SaveChanges();
        }
    }
}
