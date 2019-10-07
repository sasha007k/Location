using System.Threading.Tasks;
using Data.Models;
using System.Net.Http;
using System.Xml.Linq;
using System;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Data.Models.ParseApi;

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
            var result = await _client.GetAsync(string.Format("geocode/json?address={0}&key={1}", address.Address, apiKey));

            if (result.IsSuccessStatusCode)
            {
                var content = await result.Content.ReadAsStringAsync();
                var root = JsonConvert.DeserializeObject<RootObject>(content);

                if (root.status == "OK")
                {
                    var latitude = "";
                    var longitude = "";

                    foreach (var item in root.results)
                    {
                        latitude = item.geometry.location.lat.ToString();
                        longitude = item.geometry.location.lng.ToString();
                    }


                    LocationModel location = new LocationModel(latitude, longitude);
                    return location;
                }                
            }

                        
            return null;
        }
    }
}
