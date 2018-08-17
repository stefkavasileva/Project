using System;
using System.Collections.Generic;

namespace Landmarks.Common.Models.Main.ViewModel
{
    public class LandmarkDetailsViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string CategoryName { get; set; }

        public string RegionName { get; set; }

        public double RegionArea { get; set; }

        public int RegionPopulation { get; set; }

        public ICollection<string> ImagesPath { get; set; }

        public decimal Rating { get; set; }

        public string UserIdsRatedPic { get; set; }

        public DateTime PostedDate { get; set; }
    }
}
