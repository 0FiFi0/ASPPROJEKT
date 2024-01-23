using ASPPROJEKT.Models;
using Data;
using Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASPPROJEKT.Controllers
{
    [Route("api/authors")]
    [ApiController]
    public class AuthorApiController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AuthorApiController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("filtered")]
        public IActionResult GetFiltered(string filter)
        {
            var filteredAuthors = _context.Authors
                .Where(a => a.Name.StartsWith(filter))
                .Include(a => a.Photos)
                .ToList();

            var result = filteredAuthors.Select(author => new
            {
                author.Name,
                author.Id,
                Photos = author.Photos.Select(photo => new
                {
                    photo.PhotoId,
                    photo.Camera,
                    photo.Description,
                    photo.Resolution,
                    photo.CreatedDate,
                    photo.Format
                })
            });

            return Ok(result);
        }
    }

}

