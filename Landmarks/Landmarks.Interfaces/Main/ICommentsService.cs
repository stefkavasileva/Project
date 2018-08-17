using Landmarks.Common.Models.Main.ViewModel;
using Landmarks.Models;
using System.Linq;

namespace Landmarks.Interfaces.Main
{
    public interface ICommentsService
    {
        User GetUserByEmail(string email);

        IQueryable<CommentsViewModel> GetLandmarksComment(int landmarkId);

        void SaveComment(string userId, int landmarkId, CommentsViewModel comment);

        IQueryable<SubCommentsViewModel> GetSubComments(int commentId);

        void SaveSubComment(string userId, SubCommentsViewModel subComment, int commetnId);
    }
}
