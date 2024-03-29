﻿using System.Threading.Tasks;
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
            return View(new OperationResult<LocationModel>());
        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GetLocation(AddressModel addres)
        {
            if (string.IsNullOrEmpty(addres.Address))
            {
                return View("Location", new OperationResult<LocationModel>(false, "Please, enter address."));
            }
            var location = await _locationService.GetLocationAsync(addres);

            return View("Location", location);
        }
    }
}
