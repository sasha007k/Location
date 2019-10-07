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
        public LocationService(HttpClient client, IOptions<GoogleApiKey> options)
        {
            _client = client;
            _apiKey = options.Value;
            _client.BaseAddress = new Uri(_apiKey.Url);
            
        }

        public async Task<OperationResult<LocationModel>> GetLocationAsync(AddressModel address)
        {
            var result = await _client.GetAsync($"geocode/json?address={address.Address}&key={_apiKey.ApiKey}");

            if (result.IsSuccessStatusCode)
            {
                var content = await result.Content.ReadAsStringAsync();
                var root = JsonConvert.DeserializeObject<RootObject>(content);

                if (root.status == "OK")
                {
                    var latitude = root.results[0].geometry.location.lat.ToString();
                    var longitude = root.results[0].geometry.location.lng.ToString();

                    LocationModel location = new LocationModel(latitude, longitude);

                    OperationResult<LocationModel> coordinates = new OperationResult<LocationModel>(location, true);

                    return coordinates;
                }
            }

            OperationResult<LocationModel> coordinates1 = new OperationResult<LocationModel>(false, "This address doesn't exist. Please, try another one.");

            return coordinates1;
        }
    }
}
