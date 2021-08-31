using DreamProperties.Common.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace DreamProperties.Common.Controllers
{
    public class FakePropertyController : IPropertyController
    {
        public Task<PropertyDTO> CreateProperty(CreatePropertyDTO propertyDTO)
        {
            return Task.FromResult(new PropertyDTO());
        }

        public Task<IEnumerable<PropertyDTO>> GetAllProperties()
        {
            var popular = new List<PropertyDTO>
            {
                new PropertyDTO
                {
                    Id = 1,
                    Title = "3 Bedroom Independent House",
                    Address = "440 Lisbon Ave, Buffalo, NY 14215, United States",
                    City = "New York",
                    Price = 100000,
                    SquareMeters = 200,
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
                    Title = "Beautiful house",
                    Address = "302 Dearborn St, Buffalo, United States",
                    City = "Buffalo",
                    Price = 60000,
                    SquareMeters = 300,
                    NumberOfBedrooms = 3,
                    PropertyType = PropertyType.House,
                    ImageUrl = "propertyWide.png",
                    NumberOfLikes = 14,
                    ForSale = true
                },
                new PropertyDTO
                {
                    Id = 5,
                    Title = "Big house",
                    Address = "33 Walnut St, Montgomery, United States",
                    City = "Montgomery",
                    Price = 400000,
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

        public Task<bool> UploadImage(FileResult image, int propertyId)
        {
            return Task.FromResult(true);
        }
    }
}
