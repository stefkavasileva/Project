using System;

namespace Landmarks.Common.Models.Main.ViewModels
{
    public class LandmarkRankingViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string CategoryName { get; set; }

        public string RegionName { get; set; }

        public DateTime PostedDate { get; set; }

        public int VisitorsCount { get; set; }

        public int DesireToVisitCount{ get; set; }  
    }
}
