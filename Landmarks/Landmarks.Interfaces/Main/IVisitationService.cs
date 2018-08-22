using Landmarks.Models;

namespace Landmarks.Interfaces.Main
{
    public interface IVisitationService
    {
        Landmark GetLatmarkById(int landmarkId);

        VisitedPlaces GetCurrentVisitedPlace(int landmarkId, string userId);

        DesiredPlaces GetCurrentDesiredPlace(int landmarkId, string userId);

        void SaveEntity(int landmarkId, string userId);

        void SaveDesiredEntity(int landmarkId, string userId);

        void RemoveEntity(VisitedPlaces place);

        void RemoveEntity(DesiredPlaces place);

        bool IsVisitedByUser(int landmarkId, string userId);

        bool IsDesiredPlaceVisitedByUser(int landmarkId, string user);
    }
}
