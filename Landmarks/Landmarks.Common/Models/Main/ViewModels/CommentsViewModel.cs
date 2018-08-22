using System;
using System.ComponentModel.DataAnnotations;

namespace Landmarks.Common.Models.Main.ViewModels
{
   public class CommentsViewModel
    {
        public CommentsViewModel()
        {
            this.CommentedDate = DateTime.Now;
        }

        public int Id { get; set; }

        [Required]
        public string CommentMsg { get; set; }

        public DateTime CommentedDate { get; set; }

        public LandmarkViewModel Posts { get; set; }

        public UserViewModel Users { get; set; }
    }
}
