using Landmarks.Common.Constants;
using System.ComponentModel.DataAnnotations;

namespace Landmarks.Common.Models.Admin.BindingModels
{
    public class AddEditRegionBindingModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "msgRequiredName")]
        [MinLength(ValidationConstants.NameMinLen, ErrorMessage = "msgMinNameLen")]
        [MaxLength(ValidationConstants.NameMaxLen, ErrorMessage = "msgMaxNameLen")]
        [Display(Name = "lblName", AutoGenerateFilter = false)]
        public string Name { get; set; }

        [Required(ErrorMessage = "msgRequiredArea")]
        [Range(ValidationConstants.AreaMinValue, ValidationConstants.AreaMaxValue, ErrorMessage = "msgInvalidNumber")]
        [Display(Name = "lblArea", AutoGenerateFilter = false)]
        public double Area { get; set; }

        [Required(ErrorMessage = "msgRequiredPopulation")]
        [Range(ValidationConstants.PopulationMinValue, ValidationConstants.PopulationMaxValue, ErrorMessage = "msgInvalidNumber")]
        [Display(Name = "lblPopulation", AutoGenerateFilter = false)]
        public int Population { get; set; }
    }
}
