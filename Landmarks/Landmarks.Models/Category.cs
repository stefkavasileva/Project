using System.Collections.Generic;

namespace Landmarks.Models
{
  public  class Category
    {
        public Category()
        {
            this.Landmarks = new HashSet<Landmark>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Landmark> Landmarks { get; set; }
    }
}
