namespace Landmarks.Models
{
    public class Image
    {
        public int Id { get; set; }

        public string Path { get; set; }

        public int LandmarkId { get; set; }
    }
}
