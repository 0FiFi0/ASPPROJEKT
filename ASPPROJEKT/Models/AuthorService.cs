using System.Collections.Generic;
using System.Linq;
using System.Net;
using Data;
using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace ASPPROJEKT.Models
{
    public class AuthorService : IAuthorService
    {
        private readonly AppDbContext _context;

        public AuthorService(AppDbContext context)
        {
            _context = context;
        }

        public List<AuthorEntity> GetAllAuthors()
        {
            return _context.Authors
                .Include(a => a.Address)
                .ToList();
        }
        public AuthorEntity GetAuthorById(int id)
        {
            return _context.Authors
                .Include(a => a.Address)
                .FirstOrDefault(m => m.Id == id);
        }

        public void CreateAuthor(AuthorEntity author)
        {
            _context.Add(author);
            _context.SaveChanges();
        }
        public void UpdateAuthor(AuthorEntity author)
        {
            _context.Update(author);
            _context.SaveChanges();
        }

        public void DeleteAuthor(int id)
        {
            var author = _context.Authors.Find(id);
            if (author != null)
            {
                _context.Authors.Remove(author);
                _context.SaveChanges();
            }
        }

        public List<AddressEntity> FindAllAddressForVieModels()
        {
            return _context.Address.ToList();
        }
    }
}
