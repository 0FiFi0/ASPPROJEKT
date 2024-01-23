using ASPPROJEKT.Controllers;
using ASPPROJEKT.Models;
using Data.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ASPPROJEKT_test
{
    public class PhotoControllerTests
    {
        private PhotoController _controller;
        private IPhotoService _service;

        public PhotoControllerTests()
        {
            _service = new TestPhotoService(GetTestPhotos(), GetTestAuthors());
            _controller = new PhotoController(_service);
        }

        [Fact]
        public void PagedIndex_ReturnsViewResult_WithPagedList()
        {
            var result = _controller.PagedIndex();

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<PagingList<PhotoEntity>>(viewResult.ViewData.Model);
            Assert.NotNull(model);
        }
        [Theory]
        [InlineData(1)]
        public void PagedIndex_ReturnsCorrectPage(int page)
        {
            var result = _controller.PagedIndex(page);

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<PagingList<PhotoEntity>>(viewResult.ViewData.Model);
            Assert.Equal(page, model.Page);
        }

        [Fact]
        public void PagedIndex_ReturnsFirstPage_WhenPageIsLessThanOne()
        {
            var result = _controller.PagedIndex(-1);

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<PagingList<PhotoEntity>>(viewResult.ViewData.Model);
            Assert.Equal(1, model.Page);
        }

        [Fact]
        public void PagedIndex_ReturnsLastPage_WhenPageIsGreaterThanTotalPages()
        {
            var result = _controller.PagedIndex(10);

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<PagingList<PhotoEntity>>(viewResult.ViewData.Model);
            Assert.Equal(model.TotalPages, model.Page);
        }

        [Fact]
        public void Index_ReturnsViewResult_WithListOfPhotos()
        {
            var result = _controller.Index();

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<List<PhotoEntity>>(viewResult.ViewData.Model);
            Assert.Equal(_service.GetAllPhotos().Count, model.Count);
        }

        [Fact]
        public void Details_ForNonExistingPhoto()
        {
            var result = _controller.Details(4);

            Assert.IsType<NotFoundResult>(result);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void Details_ForExistingPhoto_ReturnsViewResultWithPhoto(int id)
        {
            var result = _controller.Details(id);

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<PhotoEntity>(viewResult.ViewData.Model);
            Assert.Equal(id, model.PhotoId);
        }

        [Fact]
        public void Create_WithValidModel_RedirectsToPagedIndex()
        {
            var photo = new PhotoEntity { Description = "TestPhoto", CreatedDate = DateTime.Now };

            var result = _controller.Create(photo);

            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("PagedIndex", redirectToActionResult.ActionName);
        }

        [Fact]
        public void Edit_WithValidModel_RedirectsToPagedIndex()
        {
            var existingPhoto = new PhotoEntity { PhotoId = 1, Description = "ExistingPhoto", CreatedDate = DateTime.Now };

            var result = _controller.Edit(existingPhoto);

            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("PagedIndex", redirectToActionResult.ActionName);
        }

        [Fact]
        public void Delete_RedirectsToPagedIndex()
        {
            var result = _controller.Delete(1);

            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("PagedIndex", redirectToActionResult.ActionName);
        }

        private List<PhotoEntity> GetTestPhotos()
        {
            return new List<PhotoEntity>
            {
                new PhotoEntity { PhotoId = 1, Description = "Photo1", CreatedDate = DateTime.Now },
                new PhotoEntity { PhotoId = 2, Description = "Photo2", CreatedDate = DateTime.Now },
                new PhotoEntity { PhotoId = 3, Description = "Photo3", CreatedDate = DateTime.Now },
            };
        }

        private List<AuthorEntity> GetTestAuthors()
        {
            return new List<AuthorEntity>
            {
                new AuthorEntity { Id = 1, Name = "Author1", Description = "Description1" },
                new AuthorEntity { Id = 2, Name = "Author2", Description = "Description2" },
                new AuthorEntity { Id = 3, Name = "Author3", Description = "Description3" },
            };
        }

        private class TestPhotoService : IPhotoService
        {
            private readonly List<PhotoEntity> _photos;
            private readonly List<AuthorEntity> _authors;

            public TestPhotoService(List<PhotoEntity> photos, List<AuthorEntity> authors)
            {
                _photos = photos;
                _authors = authors;
            }

            public List<PhotoEntity> GetAllPhotos()
            {
                return _photos;
            }

            public PhotoEntity GetPhotoById(int id)
            {
                return _photos.Find(photo => photo.PhotoId == id);
            }

            public void CreatePhoto(PhotoEntity photo)
            {
                _photos.Add(photo);
            }

            public void UpdatePhoto(PhotoEntity photo)
            {
                var existingPhoto = _photos.Find(p => p.PhotoId == photo.PhotoId);
                if (existingPhoto != null)
                {
                    existingPhoto.Description = photo.Description;
                    existingPhoto.CreatedDate = photo.CreatedDate;
                }
            }

            public void DeletePhoto(int id)
            {
                var photoToRemove = _photos.Find(p => p.PhotoId == id);
                if (photoToRemove != null)
                {
                    _photos.Remove(photoToRemove);
                }
            }

            public List<AuthorEntity> FindAllAuthorsForVieModels()
            {
                return _authors;
            }

            public PagingList<PhotoEntity> FindPage(int page, int size)
            {
                return PagingList<PhotoEntity>.Create(
                    (p, s) => _photos
                        .OrderByDescending(photo => photo.CreatedDate)
                        .Skip((p - 1) * s)
                        .Take(s)
                        .ToList(),
                    _photos.Count,
                    page,
                    size);
            }
        }
    }
}