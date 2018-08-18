namespace Landmarks.Models
{
    public class VisitedPlaces
    {
        public int LandmarkId { get; set; }

        public Landmark Landmark { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }  
    }
}
