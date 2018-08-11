using System.Collections.Generic;

namespace Landmarks.Models
{
    public class Landmark
    {
        public Landmark()
        {
            this.Images = new List<Image>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        //todo добави населено мястно 

        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public string Description { get; set; }

        public int RegionId { get; set; }

        public Region Region { get; set; }

        public ICollection<Image> Images { get; set; }  
        //otzivi

        //komnetari

        //reiting


    }
}
