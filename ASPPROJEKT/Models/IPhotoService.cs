using Data.Entities;

namespace ASPPROJEKT.Models
{
    public interface IPhotoService
    {
        List<PhotoEntity> GetAllPhotos();
        PhotoEntity GetPhotoById(int id);
        void CreatePhoto(PhotoEntity photo);
        void UpdatePhoto(PhotoEntity photo);
        void DeletePhoto(int id);
        List<AuthorEntity> FindAllAuthorsForVieModels();

        PagingList<PhotoEntity> FindPage(int page, int size);

    }
}
