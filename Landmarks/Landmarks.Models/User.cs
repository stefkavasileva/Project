using Microsoft.AspNetCore.Identity;

namespace Landmarks.Models
{
    public class User : IdentityUser
    {
        public string FullName { get; set; }
    }
}
