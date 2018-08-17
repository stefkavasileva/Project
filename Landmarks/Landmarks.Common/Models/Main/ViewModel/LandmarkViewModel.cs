using System;

namespace Landmarks.Common.Models.Main.ViewModel
{
    public class LandmarkViewModel
    {
        public int Id { get; set; }

        public string Message { get; set; }

        public DateTime PostedDate { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}
