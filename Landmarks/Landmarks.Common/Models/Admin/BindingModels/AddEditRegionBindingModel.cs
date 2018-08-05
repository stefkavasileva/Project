using System.ComponentModel.DataAnnotations;

namespace Landmarks.Common.Models.Admin.BindingModels
{
    public class AddEditRegionBindingModel
    {
        public int Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Admin.Region),ErrorMessageResourceName = "msgRequiredName")]
        [MinLength(5 , ErrorMessageResourceType = typeof(Resources.Admin.Region), ErrorMessageResourceName = "msgMinLen")]
        [MaxLength(200, ErrorMessageResourceType = typeof(Resources.Admin.Region), ErrorMessageResourceName = "msgMaxLen")]
        public string Name { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Admin.Region), ErrorMessageResourceName = "msgRequiredArea")]
        [Range(0, double.MaxValue, ErrorMessageResourceType = typeof(Resources.Admin.Region), ErrorMessageResourceName = "msgInvalidNumber")]
        public double Area { get; set; }

        [Range(0, int.MaxValue, ErrorMessageResourceType = typeof(Resources.Admin.Region), ErrorMessageResourceName = "msgInvalidNumber")]
        public int Population { get; set; }
    }
}
