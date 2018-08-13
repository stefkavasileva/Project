using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Landmarks.Models
{
    public class User : IdentityUser
    {
        public User()
        {
            this.Landmarks = new HashSet<Landmark>();
        }

        public string FullName { get; set; }

        public ICollection<Landmark> Landmarks { get; set; }
    }
}
