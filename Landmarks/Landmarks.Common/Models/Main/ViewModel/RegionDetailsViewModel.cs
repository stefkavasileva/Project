﻿using System.Collections.Generic;

namespace Landmarks.Common.Models.Main.ViewModel
{
    public class RegionDetailsViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public double Area { get; set; }

        public int Population { get; set; }

        public ICollection<LandmarkConciseViewModel> Landmarks { get; set; }
    }
}
