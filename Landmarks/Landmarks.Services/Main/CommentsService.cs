using AutoMapper;
using Landmarks.Common.Models.Main.ViewModels;
using Landmarks.Data;
using Landmarks.Interfaces.Main;
using Landmarks.Models;
using System.Linq;

namespace Landmarks.Services.Main
{
    public class CommentsService : BaseService, ICommentsService
    {
        public CommentsService(LandmarksDbContext dbContext, IMapper mapper)
            : base(dbContext, mapper)
        {
        }

        public IQueryable<CommentsViewModel> GetLandmarksComment(int landmarkId)
        {
            var comments = this.DbContext.Comments.Where(c => c.LandmarkId == landmarkId)
                                    .Select(c => new CommentsViewModel
                                    {
                                        Id = c.Id,
                                        CommentedDate = c.CommentedDate,
                                        CommentMsg = c.CommentMsg,
                                        Users = new UserViewModel
                                        {
                                            Id = c.UserId,
                                            Email = c.User.Email,
                                        }
                                    }).AsQueryable();

            return comments;
        }

        public IQueryable<SubCommentsViewModel> GetSubComments(int commentId)
        {
           var  subComments = this.DbContext.SubComments.Where(sc => sc.Comment.Id == commentId)
                                      .Select(sc => new SubCommentsViewModel
                                      {
                                          Id = sc.Id,
                                          CommentMsg = sc.CommentMsg,
                                          CommentedDate = sc.CommentedDate,
                                          User = new UserViewModel
                                          {
                                              Id = sc.User.Id,
                                              Email = sc.User.Email,
                                          }
                                      }).AsQueryable();

            return subComments;
        }

        public User GetUserByEmail(string email)
        {
            return this.DbContext.Users.FirstOrDefault(u => u.Email == email);
        }

        public void SaveComment(string userId, int landmarkId, CommentsViewModel comment)
        {
            //bool result = false;  
            Comment commentEntity = null;

            var user = this.DbContext.Users.FirstOrDefault(u => u.Id == userId);
            var post = this.DbContext.Landmarks.FirstOrDefault(p => p.Id == landmarkId);

            if (comment != null)
            {       
                commentEntity = new Comment
                {
                    CommentMsg = comment.CommentMsg,
                    CommentedDate = comment.CommentedDate,
                };

                if (user != null && post != null)
                {
                    post.Comments.Add(commentEntity);
                    user.Comments.Add(commentEntity);
                    this.DbContext.SaveChanges();  
                }
            }
        }

        public void SaveSubComment(string userId, SubCommentsViewModel subComment, int commentId)
        {
            SubComment subCommentEntity = null;
           
            var user = this.DbContext.Users.FirstOrDefault(u => u.Id == userId);
            var comment = this.DbContext.Comments.FirstOrDefault(p => p.Id == commentId);

            if (subComment != null)
            {
                subCommentEntity = new SubComment
                {
                    CommentMsg = subComment.CommentMsg,
                    CommentedDate = subComment.CommentedDate,
                };

                if (user != null && comment != null)
                {
                    comment.SubComments.Add(subCommentEntity);
                    user.SubComments.Add(subCommentEntity);

                   this.DbContext.SaveChanges(); 
                }
            }
        }
    }
}
