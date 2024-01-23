using Data.Entities;
using Data;

namespace ASPPROJEKT.Models
{
    public class AddressService : IAddressService 
    {
        private readonly AppDbContext _context;

        public AddressService(AppDbContext context)
        {
            _context = context;
        }

        public List<AddressEntity> GetAllAddress()
        {
            return _context.Address
                .ToList();
        }
        public AddressEntity GetAddressById(int id)
        {
            return _context.Address
                .FirstOrDefault(m => m.Id == id);
        }

        public void CreateAddress(AddressEntity address)
        {
            _context.Add(address);
            _context.SaveChanges();
        }
        public void UpdateAddress(AddressEntity address)
        {
            _context.Update(address);
            _context.SaveChanges();
        }

        public void DeleteAddress(int id)
        {
            var address = _context.Address.Find(id);
            if (address != null)
            {
                _context.Address.Remove(address);
                _context.SaveChanges();
            }
        }
    }
}
