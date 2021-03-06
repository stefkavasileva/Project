﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Landmarks.Common.Constants;
using System;

namespace Landmarks.Common.Models.Admin.BindingModels
{
    public class AddEditLandmarkBindingModel
    {
        public AddEditLandmarkBindingModel()
        {
            this.PostedDate = DateTime.Now;
        }

        public int Id { get; set; }

        [Required(ErrorMessage = "msgRequiredName")]
        [MinLength(ValidationConstants.NameMinLen, ErrorMessage = "msgMinNameLen")]
        [MaxLength(ValidationConstants.NameMaxLen, ErrorMessage = "msgMaxNameLen")]
        [Display(Name = "lblName", AutoGenerateFilter = false)]
        public string Name { get; set; }

        [Required(ErrorMessage = "msgRequiredCategory")]
        [Display(Name = "lblCategory", AutoGenerateFilter = false)]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "msgRequiredRegion")]
        [Display(Name = "lblRegion", AutoGenerateFilter = false)]
        public int RegionId { get; set; }

        public string CreatorId { get; set; }

        public List<SelectListItem> Categories { set; get; }

        public List<SelectListItem> Regions { set; get; }

        public DateTime PostedDate { get; set; }
    }
}
