using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Landmarks.Common.Constants;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Landmarks.Common.Models.Operator.BindingModels
{
    public class AddEditLandmarkBindingModel
    {
        public AddEditLandmarkBindingModel()
        {
            this.Images = new List<IFormFile>();
        }

        public int Id { get; set; }

        [Required(ErrorMessage = "msgRequiredName")]
        [MinLength(ValidationConstants.NameMinLen, ErrorMessage = "msgMinNameLen")]
        [MaxLength(ValidationConstants.NameMaxLen, ErrorMessage = "msgMaxNameLen")]
        [Display(Name = "lblName", AutoGenerateFilter = false)]
        public string Name { get; set; }

        [Required(ErrorMessage = "msgRequiredCategory")]
        [Display(Name = "lblCategory")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "msgRequiredRegion")]
        [Display(Name = "lblRegion")]
        public int RegionId { get; set; }

        [Display(Name = "lblDesrption")]
        public string Description { get; set; }

        public List<SelectListItem> Categories { set; get; }

        public List<SelectListItem> Regions { set; get; }

        [Display(Name = "lblImages")]
        public ICollection<IFormFile> Images { get; set; }

        public ICollection<string> ImagesPathToDisplay { get; set; }
    }
}
