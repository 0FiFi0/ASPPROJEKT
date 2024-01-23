using ASPPROJEKT.Controllers;
using ASPPROJEKT.Models;
using Data.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Xunit;

namespace ASPPROJEKT_test
{
    public class AuthorControllerTests
    {
        private AuthorController _controller;
        private IAuthorService _service;

        public AuthorControllerTests()
        {
            _service = new TestAuthorService(GetTestAuthors());
            _controller = new AuthorController(_service);
        }

        [Fact]
        public void Index_ReturnsViewResult_WithListOfAuthors()
        {
            var result = _controller.Index();

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<List<AuthorEntity>>(viewResult.ViewData.Model);
            Assert.Equal(_service.GetAllAuthors().Count, model.Count);
        }

        [Fact]
        public void Details_ForNonExistingAuthor()
        {

            var result = _controller.Details(4); 

            Assert.IsType<NotFoundResult>(result);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void Details_ForExistingAuthor_ReturnsViewResultWithAuthor(int id)
        {
            var result = _controller.Details(id);

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<AuthorEntity>(viewResult.ViewData.Model);
            Assert.Equal(id, model.Id);
        }

        [Fact]
        public void Create_WithValidModel_RedirectsToIndex()
        {
            var author = new AuthorEntity { Name = "TestAuthor", Description = "Test Description" };

            var result = _controller.Create(author);

            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }

        [Fact]
        public void Edit_WithValidModel_RedirectsToIndex()
        {
            var existingAuthor = new AuthorEntity { Id = 1, Name = "ExistingAuthor", Description = "Existing Description" };

            var result = _controller.Edit(existingAuthor);

            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }

        [Fact]
        public void Delete_RedirectsToIndex()
        {
            var result = _controller.Delete(1);

            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
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

        private class TestAuthorService : IAuthorService
        {
            private readonly List<AuthorEntity> _authors;

            public TestAuthorService(List<AuthorEntity> authors)
            {
                _authors = authors;
            }

            public List<AuthorEntity> GetAllAuthors()
            {
                return _authors;
            }

            public AuthorEntity GetAuthorById(int id)
            {
                return _authors.Find(author => author.Id == id);
            }

            public void CreateAuthor(AuthorEntity author)
            {
                _authors.Add(author);
            }

            public void UpdateAuthor(AuthorEntity author)
            {
                var existingAuthor = _authors.Find(a => a.Id == author.Id);
                if (existingAuthor != null)
                {
                    existingAuthor.Name = author.Name;
                    existingAuthor.Description = author.Description;
                }
            }

            public void DeleteAuthor(int id)
            {
                var authorToRemove = _authors.Find(a => a.Id == id);
                if (authorToRemove != null)
                {
                    _authors.Remove(authorToRemove);
                }
            }

            public List<AddressEntity> FindAllAddressForVieModels()
            {
                return new List<AddressEntity>();
            }
        }
    }
}
