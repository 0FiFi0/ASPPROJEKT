using Data.Entities;

namespace ASPPROJEKT.Models
{
    public interface IAuthorService
    {
        List<AuthorEntity> GetAllAuthors();
        AuthorEntity GetAuthorById(int id);
        void CreateAuthor(AuthorEntity author);
        void UpdateAuthor(AuthorEntity author);
        void DeleteAuthor(int id);
        List<AddressEntity> FindAllAddressForVieModels();

    }
}
