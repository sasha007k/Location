using System.Threading.Tasks;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Services.Services;


namespace LocationWEB.Controllers
{
    public class LocationController : Controller
    {
        private readonly ILocationService _locationService;
        private static LocationModel loc = new LocationModel();

        public LocationController(ILocationService locationService)
        {
            _locationService = locationService;
        }

        public IActionResult Location()
        {
            return View(loc);
        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EnterAddress(AddressModel addres)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Location");
            }

            var location = await _locationService.GetLocation(addres);
            if (location == null)
            {
                return RedirectToAction("Location");
            }
            loc = location;
            loc.Address = addres.Address;

            return RedirectToAction("Location");
        }
    }
}
