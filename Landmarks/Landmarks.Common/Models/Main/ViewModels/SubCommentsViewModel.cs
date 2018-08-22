using System;

namespace Landmarks.Common.Models.Main.ViewModels
{
    public class SubCommentsViewModel
    {
        public SubCommentsViewModel()
        {
            this.CommentedDate = DateTime.Now;
        }

        public int Id { get; set; }

        public string CommentMsg { get; set; }

        public DateTime CommentedDate { get; set; }

        public CommentsViewModel Comment { get; set; }

        public UserViewModel User { get; set; }
    }
}
