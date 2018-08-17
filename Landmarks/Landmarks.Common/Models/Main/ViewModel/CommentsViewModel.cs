using System;
namespace Landmarks.Common.Models.Main.ViewModel
{
   public class CommentsViewModel
    {
        public CommentsViewModel()
        {
            this.CommentedDate = DateTime.Now;
        }

        public int Id { get; set; }

        public string CommentMsg { get; set; }

        public DateTime CommentedDate { get; set; }

        public LandmarkViewModel Posts { get; set; }

        public UserViewModel Users { get; set; }
    }
}
