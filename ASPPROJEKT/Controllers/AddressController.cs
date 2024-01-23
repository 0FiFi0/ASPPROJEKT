using ASPPROJEKT.Models;
using Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ASPPROJEKT.Controllers
{
    public class AddressController : Controller
    {
        private readonly IAddressService _addressService;

        public AddressController(IAddressService addressService)
        {
            _addressService = addressService;
        }
        
        [Authorize(Roles = "user,admin")]
        public IActionResult Index()
        {
            var address = _addressService.GetAllAddress();
            return View(address);
        }

        [HttpGet]
        [Authorize(Roles = "user,admin")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "user,admin")]
        public IActionResult Create(AddressEntity address)
        {
            if (ModelState.IsValid)
            {
                _addressService.CreateAddress(address);
                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public IActionResult Edit(int id)
        {
            return View(_addressService.GetAddressById(id));
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public IActionResult Edit(AddressEntity address)
        {
            if (ModelState.IsValid)
            {
                _addressService.UpdateAddress(address);
                return RedirectToAction("Index");
            }
            return View();
        }


        [HttpGet]
        [Authorize(Roles = "admin")]
        public IActionResult Delete(int? id)
        {
            var address = _addressService.GetAddressById(id.Value);

            if (address == null)
            {
                return NotFound();
            }

            return View(address);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public IActionResult Delete(int id)
        {
            _addressService.DeleteAddress(id);
            return RedirectToAction("Index");
        }
    }
}
