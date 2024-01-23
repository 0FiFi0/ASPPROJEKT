using ASPPROJEKT.Models;
using Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ASPPROJEKT.Controllers
{
    public class AuthorController : Controller
    {
        private readonly IAuthorService _authorService;

        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }
        [Authorize(Roles = "user,admin")]
        public IActionResult Index()
        {
            var authors = _authorService.GetAllAuthors();
            return View(authors);
        }

        [Authorize(Roles = "admin")]
        public IActionResult Details(int? id)
        {

            var author = _authorService.GetAuthorById(id.Value);
            if (author == null)
            {
                return NotFound();
            }

            return View(author);
        }
        [HttpGet]
        [Authorize(Roles = "user,admin")]
        public IActionResult Create()
        {
            var addressList = _authorService.FindAllAddressForVieModels();

            ViewBag.AddressList = addressList;
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "user,admin")]
        public IActionResult Create(AuthorEntity author)
        {
            if (ModelState.IsValid)
            {
                _authorService.CreateAuthor(author);
                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public IActionResult Edit(int id)
        {
            var addressList = _authorService.FindAllAddressForVieModels();

            ViewBag.AddressList = addressList;
            return View(_authorService.GetAuthorById(id));
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public IActionResult Edit(AuthorEntity author)
        {
            if (ModelState.IsValid)
            {
                _authorService.UpdateAuthor(author);
                return RedirectToAction("Index");
            }
            return View();
        }


        [HttpGet]
        [Authorize(Roles = "admin")]
        public IActionResult Delete(int? id)
        {
            var author = _authorService.GetAuthorById(id.Value);

            if (author == null)
            {
                return NotFound();
            }

            return View(author);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public IActionResult Delete(int id)
        {
            _authorService.DeleteAuthor(id);
            return RedirectToAction("Index");
        }
    }
}

