using System;

namespace Landmarks.Models
{
    public class Image
    {
        public Image()
        {
            this.CreatedDate = DateTime.Now;
        }

        public int Id { get; set; }

        public string Path { get; set; }

        public int LandmarkId { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
