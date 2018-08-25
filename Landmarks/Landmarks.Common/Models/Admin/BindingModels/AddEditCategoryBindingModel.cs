using Landmarks.Common.Constants;
using System.ComponentModel.DataAnnotations;

namespace Landmarks.Common.Models.Admin.BindingModels
{
    public class AddEditCategoryBindingModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "msgRequiredName")]
        [MinLength(ValidationConstants.NameMinLen, ErrorMessage = "msgMinNameLen")]
        [MaxLength(ValidationConstants.NameMaxLen, ErrorMessage = "msgMaxNameLen")]
        [Display(Name = "lblName", AutoGenerateFilter = false)]
        public string Name { get; set; }
		
    }
}
