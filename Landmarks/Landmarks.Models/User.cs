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
        }

        public string FullName { get; set; }

        public ICollection<Landmark> Landmarks { get; set; }

        public ICollection<Comment> Comments { get; set; }

        public ICollection<SubComment> SubComments { get; set; }
    }
}
