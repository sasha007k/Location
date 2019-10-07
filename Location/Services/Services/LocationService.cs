using System.Threading.Tasks;
using Data.Models;
using System.Net.Http;
using System;
using Newtonsoft.Json;
using Data.Models.ParseApi;
using Microsoft.Extensions.Options;

namespace Services.Services
{
    public class LocationService : ILocationService
    {
        private readonly HttpClient _client;
        private const string _baseUrl = "https://maps.googleapis.com/maps/api/";
        private readonly GoogleApiKey _apiKey;
        public LocationService(HttpClient client, IOptionsMonitor<GoogleApiKey> options)
        {
            _client = client;
            _client.BaseAddress = new Uri(_baseUrl);
            _apiKey = options.CurrentValue;
        }
        
        public async Task<LocationModel> GetLocationAsync(AddressModel address)
        {
            var result = await _client.GetAsync(string.Format("geocode/json?address={0}&key={1}", address.Address, _apiKey.ApiKey));

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
