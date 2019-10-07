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
        private readonly GoogleApiKey _apiKey;
        public LocationService(HttpClient client, IOptionsMonitor<GoogleApiKey> options)
        {
            _client = client;
            _apiKey = options.CurrentValue;
            _client.BaseAddress = new Uri(_apiKey.Url);
            
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
                    var latitude = root.results[0].geometry.location.lat.ToString();
                    var longitude = root.results[0].geometry.location.lng.ToString();

                    LocationModel location = new LocationModel(latitude, longitude);
                    return location;
                }                
            }

                        
            return null;
        }
    }
}
