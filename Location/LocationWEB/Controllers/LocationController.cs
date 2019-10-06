using System.Threading.Tasks;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Services.Services;


namespace LocationWEB.Controllers
{
    public class LocationController : Controller
    {
        private readonly ILocationService _locationService;

        public LocationController(ILocationService locationService)
        {
            _locationService = locationService;
        }

        public IActionResult Location()
        {
            return View(new LocationModel());
        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GetLocation(AddressModel addres)
        {
            var location = await _locationService.GetLocationAsync(addres);
            if (location == null)
            {
                return View("Location", new LocationModel());
            }
            location.Address = addres.Address;

            return View("Location", location);
        }
    }
}
