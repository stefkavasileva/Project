using System;
using System.Collections.Generic;

namespace Landmarks.Models
{
    public class Comment
    {
        public Comment()
        {
            this.SubComments = new HashSet<SubComment>();
        }

        public int Id { get; set; }

        public string CommentMsg { get; set; }

        public DateTime CommentedDate { get; set; }

        public int LandmarkId{ get; set; }

        public string UserId { get; set; }

        public Landmark Landmark { get; set; }

        public User User { get; set; }

        public ICollection<SubComment> SubComments { get; set; }
    }
}
