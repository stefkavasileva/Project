using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Landmarks.Common.Models.Admin.BindingModels
{
    public class AddEditLandmarkBindingModel
    {
        public int Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Admin.Landmark), ErrorMessageResourceName = "msgRequiredName")]
        [MinLength(5, ErrorMessageResourceType = typeof(Resources.Admin.Landmark), ErrorMessageResourceName = "msgMinLen")]
        [MaxLength(200, ErrorMessageResourceType = typeof(Resources.Admin.Landmark), ErrorMessageResourceName = "msgMaxLen")]
        public string Name { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        public int RegionId { get; set; }   

        public List<SelectListItem> Categories { set; get; }

        public List<SelectListItem> Regions { set; get; }

    }
}
