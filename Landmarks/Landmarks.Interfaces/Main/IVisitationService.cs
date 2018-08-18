using Landmarks.Models;

namespace Landmarks.Interfaces.Main
{
    public interface IVisitationService
    {
        void SaveEntity(int landmark, string userId);

        Landmark GetLatmarkById(int landmarkId);

        VisitedPlaces GetCurrentVisitedPlace(int landmarkId, string userId);

        void RemoveEntity(VisitedPlaces place);

        bool IsVisitedByUser(int landmarkId, string userId);
    }
}
