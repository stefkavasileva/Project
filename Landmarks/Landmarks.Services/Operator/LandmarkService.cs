using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
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
    public class LandmarkService : BaseService, ILandmarkService
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
            //var landmark = this.Mapper.Map<AddEditLandmarkBindingModel, Landmark>(model);

            var landmark = new Landmark
            {
                Name = model.Name,
                CategoryId = model.CategoryId,
                Description = model.Description,
                RegionId = model.RegionId,
                CreatorId = model.CreatorId,
            };

            this.DbContext.Landmarks.Add(landmark);
            this.DbContext.SaveChanges();

            if (!imagesPaths.Any()) return;

            var imagesToDb = new List<Image>();
            foreach (var imagesPath in imagesPaths)
            {
                imagesToDb.Add(new Image { LandmarkId = landmark.Id, Path = imagesPath });
            }

            this.DbContext.Images.AddRange(imagesToDb);
            this.DbContext.SaveChanges();

            landmark.Images = imagesToDb;

            this.DbContext.SaveChanges();
        }

        public void SaveEntity(AddEditLandmarkBindingModel model, List<string> imagesPaths)
        {
            var landmark = this.DbContext.Landmarks.Include(l => l.Images).FirstOrDefault(l => l.Id == model.Id);

            //remove old picture from database and server
            RemoveImagesFromServer(landmark);
            var imagesForDelete = this.DbContext.Images.Where(i => i.LandmarkId == model.Id);
            this.DbContext.Images.RemoveRange(imagesForDelete);
            this.DbContext.SaveChanges();

            var imagesToDb = new List<Image>();

            if (imagesPaths.Any())
            {
                foreach (var imagesPath in imagesPaths)
                {
                    imagesToDb.Add(new Image { LandmarkId = model.Id, Path = imagesPath });
                }

                this.DbContext.Images.AddRange(imagesToDb);
                this.DbContext.SaveChanges();
            }

            landmark.Name = model.Name;
            landmark.CategoryId = model.CategoryId;
            landmark.Description = model.Description;
            landmark.RegionId = model.RegionId;
            landmark.Images = imagesToDb;

            this.DbContext.Attach(landmark).State = EntityState.Modified;
            this.DbContext.SaveChanges();
        }

        public IQueryable<LandmarkConciseViewModel> GetLandmarksByCreatorId(string id)
        {
            var dbLandmarks = this.DbContext
                .Landmarks
                .Where(l => l.CreatorId == id)
                .Include(l => l.Region)
                .Include(l => l.Category);

            var modelCollection = this.Mapper.Map<ICollection<LandmarkConciseViewModel>>(dbLandmarks).AsQueryable();

            return modelCollection;
        }

        public Landmark GetLandmark(int id)
        {
            return this.DbContext.Landmarks.Include(l => l.Images).FirstOrDefault(l => l.Id == id);
        }

        public void DeleteLandmark(Landmark landmark)
        {
            //remove child
            RemoveImagesFromServer(landmark);

            this.DbContext.Remove(landmark);
            this.DbContext.SaveChanges();
        }

        private void RemoveImagesFromServer(Landmark landmark)
        {
            foreach (var landmarkImage in landmark.Images)
            {
                var image = this.DbContext.Images.FirstOrDefault(i => i.Id == landmarkImage.Id);
                if (image == null) continue;

                var newPath = Regex.Replace(image.Path, "/", "\\").Substring(1);
                var fullFilePath = Path.Combine(_environment.WebRootPath + newPath);

                if (File.Exists(fullFilePath))
                {
                    File.Delete(fullFilePath);
                }

                this.DbContext.Images.Remove(image);
            }
        }
    }
}
