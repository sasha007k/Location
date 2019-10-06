using System.Threading.Tasks;
using Data.Models;
using System.Net.Http;
using System.Xml.Linq;

namespace Services.Services
{
    public class LocationService : ILocationService
    {
        public async Task<LocationModel> GetLocationAsync(AddressModel address)
        {
            string apiKey = "AIzaSyAqAed3TFiripsTx_CL_ReK3jOoBDqmvF0";

            var requestUri = string.Format("https://maps.googleapis.com/maps/api/geocode/xml?address={0}&key={1}", address.Address, apiKey);

            if (address.Address != null)
            {
                using (var client = new HttpClient())
                {
                    var request = await client.GetAsync(requestUri);
                    var content = await request.Content.ReadAsStringAsync();
                    var xmlDocument = XDocument.Parse(content);


                    XElement result = xmlDocument.Element("GeocodeResponse").Element("result");
                    XElement locationElement = result.Element("geometry").Element("location");
                    string latitude = (string)locationElement.Element("lat");
                    string longitude = (string)locationElement.Element("lng");

                    LocationModel location = new LocationModel(latitude, longitude);
                    return location;
                }
            }
            return null;
        }
    }
}
