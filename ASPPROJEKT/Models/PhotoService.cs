using System.Collections.Generic;
using System.Linq;
using Data;
using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace ASPPROJEKT.Models
{
    public class PhotoService : IPhotoService
    {
        private readonly AppDbContext _context;

        public PhotoService(AppDbContext context)
        {
            _context = context;
        }

        public List<PhotoEntity> GetAllPhotos()
        {
            return _context.Photos
                .Include(a => a.Author)
                .ToList();
        }
        public PhotoEntity GetPhotoById(int id)
        {
            return _context.Photos
                .Include(a => a.Author)
                .ThenInclude(d => d.Address)
                .FirstOrDefault(m => m.PhotoId == id);
        }

        public void CreatePhoto(PhotoEntity photo)
        {
            _context.Add(photo);
            _context.SaveChanges();
        }
        public void UpdatePhoto(PhotoEntity photo)
        {
            _context.Update(photo);
            _context.SaveChanges();
        }

        public void DeletePhoto(int id)
        {
            var photo = _context.Photos.Find(id);
            if (photo != null)
            {
                _context.Photos.Remove(photo);
                _context.SaveChanges();
            }
        }

        public List<AuthorEntity> FindAllAuthorsForVieModels()
        {
            return _context.Authors.ToList();
        }

        public PagingList<PhotoEntity> FindPage(int page, int size)
        {
            return PagingList<PhotoEntity>.Create(
                (p, s) => _context.Photos
                    .Include(a => a.Author)
                    .OrderByDescending(photo => photo.CreatedDate)
                    .Skip((p - 1) * s)
                    .Take(s)
                    .ToList(),
                _context.Photos.Count(),
                page,
                size);
        }
    }
}
