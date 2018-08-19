using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Landmarks.Models
{
    public class User : IdentityUser
    {
        public User()
        {
            this.Landmarks = new HashSet<Landmark>();
            this.Comments = new HashSet<Comment>();
            this.SubComments = new HashSet<SubComment>();
            this.VisitedPlaces = new HashSet<VisitedPlaces>();
        }

        public string FullName { get; set; }

        public ICollection<Landmark> Landmarks { get; set; }

        public ICollection<Comment> Comments { get; set; }

        public ICollection<SubComment> SubComments { get; set; }

        public ICollection<VisitedPlaces> VisitedPlaces { get; set; }

        public ICollection<DesiredPlaces> DesiredPlaces { get; set; }
    }
}
