using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Landmarks.Common.Constants;

namespace Landmarks.Common.Models.Admin.BindingModels
{
    public class AddEditLandmarkBindingModel
    {
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

        public List<SelectListItem> Categories { set; get; }

        public List<SelectListItem> Regions { set; get; }

    }
}
