using Landmarks.Common.Models.Main.ViewModels;
using Landmarks.Models;
using System.Linq;

namespace Landmarks.Interfaces.Main
{
    public interface ICommentsService
    {
        User GetUserByEmail(string email);

        IQueryable<CommentsViewModel> GetLandmarksComment(int landmarkId);

        IQueryable<SubCommentsViewModel> GetSubComments(int commentId);

        void SaveComment(string userId, int landmarkId, CommentsViewModel comment);

        void SaveSubComment(string userId, SubCommentsViewModel subComment, int commetnId);
    }
}
