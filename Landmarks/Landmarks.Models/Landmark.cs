using System;
using System.Collections.Generic;

namespace Landmarks.Models
{
    public class Landmark
    {
        public Landmark()
        {
            this.Images = new HashSet<Image>();
            this.Comments = new HashSet<Comment>();
            this.PostedDate = DateTime.Now;
            this.Visitors = new List<VisitedPlaces>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public string Description { get; set; }

        public int RegionId { get; set; }

        public Region Region { get; set; }

        public ICollection<Image> Images { get; set; }

        public string CreatorId { get; set; }

        public User Creator { get; set; }

        public decimal RatingSum { get; set; }

        public int RatingCount { get; set; }

        public decimal Rating { get; set; }

        public string UserIdsRatedPic { get; set; } = string.Empty;

        public ICollection<Comment> Comments { get; set; }

        public DateTime PostedDate { get; set; }

        public ICollection<VisitedPlaces> Visitors { get; set; }
    }
}
