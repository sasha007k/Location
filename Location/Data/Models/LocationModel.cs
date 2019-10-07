namespace Data.Models
{
    public class LocationModel
    {
        public string Latitude { get; set; }

        public string Longitude { get; set; }

        public LocationModel()
        { }
        public LocationModel(string latitude, string longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }
    }
}
