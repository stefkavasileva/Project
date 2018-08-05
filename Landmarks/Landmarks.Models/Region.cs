using System.Collections.Generic;

namespace Landmarks.Models
{
    public class Region
    {
        public Region()
        {
            this.Landmarks = new HashSet<Landmark>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public double Area { get; set; }

        public int Population { get; set; }

        public ICollection<Landmark> Landmarks { get; set; }
    }
}
