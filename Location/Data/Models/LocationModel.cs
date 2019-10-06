namespace Data.Models
{
    public class LocationModel
    {
        public string Latitude { get; set; }

        public string Longitude { get; set; }

        public string Address { get; set; }

        public LocationModel()
        { }
        public LocationModel(string lat, string lng)
        {
            Latitude = lat;
            Longitude = lng;
        }
    }
}
