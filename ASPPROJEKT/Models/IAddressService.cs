using Data.Entities;

namespace ASPPROJEKT.Models
{
    public interface IAddressService
    {
        List<AddressEntity> GetAllAddress();
        AddressEntity GetAddressById(int id);
        void CreateAddress(AddressEntity address);
        void UpdateAddress(AddressEntity address);
        void DeleteAddress(int id);

    }
}
