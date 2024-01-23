using ASPPROJEKT.Controllers;
using ASPPROJEKT.Models;
using Data.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPPROJEKT_test
{
    public class AddressControllerTests
    {
        private AddressController _controller;
        private IAddressService _service;

        public AddressControllerTests()
        {
            _service = new TestAddressService(GetTestAddresses());
            _controller = new AddressController(_service);
        }

        [Fact]
        public void Index_ReturnsViewResult_WithListOfAddresses()
        {
            var result = _controller.Index();

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<List<AddressEntity>>(viewResult.ViewData.Model);
            Assert.Equal(_service.GetAllAddress().Count, model.Count);
        }

        [Fact]
        public void CreatePost_WithValidModel_RedirectsToIndex()
        {
            var address = new AddressEntity { City = "TestCity", Street = "TestStreet", PostalCode = "12345" };

            var result = _controller.Create(address);

            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }

        [Fact]
        public void Edit_WithValidModel_RedirectsToIndex()
        {
            var existingAddress = new AddressEntity { Id = 1, City = "ExistingCity", Street = "ExistingStreet", PostalCode = "54321" };

            var result = _controller.Edit(existingAddress);

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

        private List<AddressEntity> GetTestAddresses()
        {
            return new List<AddressEntity>
            {
                new AddressEntity { Id = 1, City = "City1", Street = "Street1", PostalCode = "11111" },
                new AddressEntity { Id = 2, City = "City2", Street = "Street2", PostalCode = "22222" },
                new AddressEntity { Id = 3, City = "City3", Street = "Street3", PostalCode = "33333" },
            };
        }

        private class TestAddressService : IAddressService
        {
            private readonly List<AddressEntity> _addresses;

            public TestAddressService(List<AddressEntity> addresses)
            {
                _addresses = addresses;
            }

            public List<AddressEntity> GetAllAddress()
            {
                return _addresses;
            }

            public AddressEntity GetAddressById(int id)
            {
                return _addresses.Find(address => address.Id == id);
            }

            public void CreateAddress(AddressEntity address)
            {
                _addresses.Add(address);
            }

            public void UpdateAddress(AddressEntity address)
            {
                var existingAddress = _addresses.Find(a => a.Id == address.Id);
                if (existingAddress != null)
                {
                    existingAddress.City = address.City;
                    existingAddress.Street = address.Street;
                    existingAddress.PostalCode = address.PostalCode;
                }
            }

            public void DeleteAddress(int id)
            {
                var addressToRemove = _addresses.Find(a => a.Id == id);
                if (addressToRemove != null)
                {
                    _addresses.Remove(addressToRemove);
                }
            }
        }
    }
}
