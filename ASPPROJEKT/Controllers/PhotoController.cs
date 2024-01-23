using System.Collections.Generic;
using ASPPROJEKT.Models;
using Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ASPPROJEKT.Controllers
{
    public class PhotoController : Controller
    {
        private const int PageSize = 3;

        private readonly IPhotoService _photoService;

        public PhotoController(IPhotoService photoService)
        {
            _photoService = photoService;
        }

        [Authorize(Roles = "user,admin")]
        public IActionResult Index()
        {
            var photos = _photoService.GetAllPhotos();
            return View(photos);
        }

        public IActionResult Details(int? id)
        {

            var photo = _photoService.GetPhotoById(id.Value);
            if (photo == null)
            {
                return NotFound();
            }

            return View(photo);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var authorList = _photoService.FindAllAuthorsForVieModels();

            ViewBag.AuthorList = authorList;
            return View();
        }

        [HttpPost]       
        public IActionResult Create(PhotoEntity photo)
        {
            if (ModelState.IsValid)
            {
                _photoService.CreatePhoto(photo);
                return RedirectToAction("PagedIndex");
            }

            return View();
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public IActionResult Edit(int id)
        {
            var authorList = _photoService.FindAllAuthorsForVieModels();

            ViewBag.AuthorList = authorList;
            return View(_photoService.GetPhotoById(id));
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public IActionResult Edit(PhotoEntity photo)
        {
            if (ModelState.IsValid)
            {
                _photoService.UpdatePhoto(photo);
                return RedirectToAction("PagedIndex");
            }
                return View();
        }


        [HttpGet]
        [Authorize(Roles = "admin")]
        public IActionResult Delete(int? id)
        {
            var photo = _photoService.GetPhotoById(id.Value);

            if (photo == null)
            {
                return NotFound();
            }

            return View(photo);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public IActionResult Delete(int id)
        {
            _photoService.DeletePhoto(id);
            return RedirectToAction("PagedIndex");
        }

        public IActionResult PagedIndex(int page = 1)
        {
            return View(_photoService.FindPage(page, PageSize));
        }



    }
}
