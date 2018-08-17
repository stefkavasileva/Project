using System;

namespace Landmarks.Common.Models.Main.ViewModel
{
    public class LandmarkConciseViewModel
    {
        public int LandmarkId { get; set; }

        public string Name { get; set; }

        public string CategoryName { get; set; }

        public string RegionName { get; set; }

        public string Description { get; set; }

        public string MainPagePath { get; set; }

        public DateTime CreatedDate { get; set; }

        public string CreatorEmail { get; set; }
    }
}
