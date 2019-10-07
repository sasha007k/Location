using System.Threading.Tasks;
using Data.Models;
using System.Net.Http;
using System.Xml.Linq;
using System;

namespace Services.Services
{
    public class LocationService : ILocationService
    {
        private readonly HttpClient _client;
        private const string _baseUrl = "https://maps.googleapis.com/maps/api/";
        public LocationService(HttpClient client)
        {
            _client = client;
            _client.BaseAddress = new Uri(_baseUrl);
        }
        
        public async Task<LocationModel> GetLocationAsync(AddressModel address)
        {
            string apiKey = "AIzaSyAqAed3TFiripsTx_CL_ReK3jOoBDqmvF0";
            var result = await _client.GetAsync(string.Format("geocode/xml?address={0}&key={1}", address.Address, apiKey));

            if (result.IsSuccessStatusCode)
            {
                var content = await result.Content.ReadAsStringAsync();
                var xmlDocument = XDocument.Parse(content);


                XElement parse = xmlDocument.Element("GeocodeResponse").Element("result");
                XElement locationElement = parse.Element("geometry").Element("location");
                string latitude = (string)locationElement.Element("lat");
                string longitude = (string)locationElement.Element("lng");

                LocationModel location = new LocationModel(latitude, longitude);
                return location;
            }

                        
            return null;
        }
    }
}
