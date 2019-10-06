using System.Threading.Tasks;
using Data.Models;
using OpenCage.Geocode;

namespace Services.Services
{
    public class LocationService : ILocationService
    {
        public async Task<LocationModel> GetLocation(AddressModel address)
        {
            string key = "9c0b941084e74fdd8ffc27cf8625b566";

            var gc = new Geocoder(key);
            var result = gc.Geocode(address.Address);

            if (result.Status.Message == "OK")
            {
                var results = result.Results;
                
                if (results.Length == 0)
                {
                    return null;             
                }
                var dms = results[0].Annotations.DMS;
                var latitude = dms.Lat;
                var longitude = dms.Lng;
                LocationModel loc = new LocationModel(latitude, longitude);

                return loc;
            }

            return null;
        }
    }
}
