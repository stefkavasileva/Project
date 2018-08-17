using System;

namespace Landmarks.Models
{
    public class SubComment
    {
        public int Id { get; set; }

        public string CommentMsg { get; set; }

        public DateTime CommentedDate { get; set; }

        public int CommentId { get; set; }

        public string UserId { get; set; }

        public Comment Comment { get; set; }

        public User User { get; set; }
    }
}
