using System.ComponentModel.DataAnnotations;

namespace Landmarks.Common.Models.Admin.BindingModels
{
    public class AddEditCategoryBindingModel
    {
        public int Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Admin.Category), ErrorMessageResourceName = "msgRequiredName")]
        [MinLength(5, ErrorMessageResourceType = typeof(Resources.Admin.Category), ErrorMessageResourceName = "msgMinLen")]
        [MaxLength(200, ErrorMessageResourceType = typeof(Resources.Admin.Category), ErrorMessageResourceName = "msgMaxLen")]
        public string Name { get; set; }   
    }
}
