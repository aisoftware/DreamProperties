using DreamProperties.Common.Models;
using DreamProperties.Common.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DreamProperties.Common.Controllers
{
    public interface IPropertyController
    {
        Task<IEnumerable<PropertyDTO>> GetAllProperties();
        Task<IEnumerable<PropertyDTO>> GetProperties(SearchQuery searchQuery);
    }

    public class PropertyController : IPropertyController
    {
        private INetworkService _networkService;

        public PropertyController(INetworkService networkService)
        {
            _networkService = networkService;
        }

        public async Task<IEnumerable<PropertyDTO>> GetAllProperties()
        {
            var properties = await _networkService.GetAsync<List<PropertyDTO>>(Constants.API_URL + "property");

            return properties;
        }

        public async Task<IEnumerable<PropertyDTO>> GetProperties(SearchQuery searchQuery)
        {
            string parameter = string.Empty;

            if (searchQuery.SearchType == SearchType.City)
            {
                parameter = $"city={Uri.EscapeDataString(searchQuery.Term)}";
            }
            else
            {
                parameter = $"type={searchQuery.Term}";
            }

            var properties = await _networkService.GetAsync<List<PropertyDTO>>(Constants.API_URL + $"property/query?{parameter}");

            return properties;
        }
    }

    public class FakePropertyController : IPropertyController
    {
        public Task<IEnumerable<PropertyDTO>> GetAllProperties()
        {
            var popular = new List<PropertyDTO>
            {
                new PropertyDTO
                {
                    Id = 1,
                    Title = "3 Bedroom Independent House",
                    Address = "132, West street, New York, United States",
                    City = "New York",
                    Price = 40500,
                    SquareMeters = 1800,
                    NumberOfBedrooms = 3,
                    PropertyType = PropertyType.House,
                    ImageUrl = "propertyWide.png",
                    NumberOfLikes = 88,
                    ForSale = true
                },
                new PropertyDTO
                {
                    Id = 2,
                    Title = "2 Bedroom Flat",
                    Address = "132, Main street, Los Angeles, United States",
                    City = "Los Angeles",
                    Price = 1800,
                    SquareMeters = 50,
                    NumberOfBedrooms = 2,
                    PropertyType = PropertyType.Flat,
                    ImageUrl = "propertyWide.png",
                    NumberOfLikes = 75,
                    ForSale = false
                },
                new PropertyDTO
                {
                    Id = 3,
                    Title = "3 Bedroom Flat",
                    Address = "132, West street, New York, United States",
                    City = "New York",
                    Price = 2200,
                    SquareMeters = 60,
                    NumberOfBedrooms = 3,
                    PropertyType = PropertyType.Flat,
                    ImageUrl = "propertyWide.png",
                    NumberOfLikes = 66,
                    ForSale = false
                },
                new PropertyDTO
                {
                    Id = 4,
                    Title = "3 Bedroom Independent House",
                    Address = "132, West street, New York, United States",
                    City = "New York",
                    Price = 40500,
                    SquareMeters = 1800,
                    NumberOfBedrooms = 3,
                    PropertyType = PropertyType.House,
                    ImageUrl = "propertyWide.png",
                    NumberOfLikes = 14,
                    ForSale = true
                },
                new PropertyDTO
                {
                    Id = 5,
                    Title = "3 Bedroom Independent House",
                    Address = "132, West street, New York, United States",
                    City = "New York",
                    Price = 40500,
                    SquareMeters = 1000,
                    NumberOfBedrooms = 3,
                    PropertyType = PropertyType.House,
                    ImageUrl = "propertyWide.png",
                    NumberOfLikes = 2,
                    ForSale = true
                },
            };

            return Task.FromResult(popular.AsEnumerable());
        }

        public Task<IEnumerable<PropertyDTO>> GetProperties(SearchQuery searchQuery)
        {
            return GetAllProperties();
        }
    }
}
