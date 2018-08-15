using System.Collections.Generic;
using Landmarks.Common.Constants;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Landmarks.Common.Models.Admin.BindingModels
{
    public class AddEditRegionBindingModel
    {
        public int Id { get; set; }

        //[Required(ErrorMessage = "msgRequiredName")]
        //[MinLength(ValidationConstants.NameMinLen, ErrorMessage = "msgMinNameLen")]
        //[MaxLength(ValidationConstants.NameMaxLen, ErrorMessage = "msgMaxNameLen")]
        //[Display(Name = "lblName")]
        //public string Name { get; set; }

        public int Name { get; set; }

        public List<SelectListItem> Regions { set; get; }

        [Required(ErrorMessage = "msgRequiredArea")]
        [Range(ValidationConstants.AreaMinValue, ValidationConstants.AreaMaxValue, ErrorMessage = "msgInvalidNumber")]
        [Display(Name = "lblArea")]
        public double Area { get; set; }

        [Required(ErrorMessage = "msgRequiredPopulation")]
        [Range(ValidationConstants.PopulationMinValue, ValidationConstants.PopulationMaxValue, ErrorMessage = "msgInvalidNumber")]
        [Display(Name = "lblPopulation")]
        public int Population { get; set; }
    }
}
