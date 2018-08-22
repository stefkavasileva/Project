using System.Collections.Generic;

namespace Landmarks.Common.Models.Main.ViewModels
{
    public class LandmarksByCategoryViewModel
    {
        public string Name { get; set; }

        public ICollection<LandmarkConciseViewModel> Landmarks { get; set; }
    }
}
