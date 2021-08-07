using DreamProperties.Common.Models;
using DreamProperties.Common.Network;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace DreamProperties.Common.Controllers
{
    //public class FakePropertyController
    //{

    //    public Task<IEnumerable<PropertyDTO>> GetAllProperties()
    //    {
    //        var popular = new List<PropertyDTO>
    //        {
    //            new PropertyDTO
    //            {
    //                Id = 1,
    //                Title = "3 Bedroom Independent House",
    //                Address = "132, West street, New York, United States",
    //                City = "New York",
    //                Price = 40500,
    //                SquareMeters = 1800,
    //                NumberOfBedrooms = 3,
    //                PropertyType = PropertyType.House,
    //                ImageUrl = "propertyWide.png",
    //                NumberOfLikes = 88,
    //                ForSale = true
    //            },
    //            new PropertyDTO
    //            {
    //                Id = 2,
    //                Title = "2 Bedroom Flat",
    //                Address = "132, Main street, Los Angeles, United States",
    //                City = "Los Angeles",
    //                Price = 1800,
    //                SquareMeters = 50,
    //                NumberOfBedrooms = 2,
    //                PropertyType = PropertyType.Flat,
    //                ImageUrl = "propertyWide.png",
    //                NumberOfLikes = 75,
    //                ForSale = false
    //            },
    //            new PropertyDTO
    //            {
    //                Id = 3,
    //                Title = "3 Bedroom Flat",
    //                Address = "132, West street, New York, United States",
    //                City = "New York",
    //                Price = 2200,
    //                SquareMeters = 60,
    //                NumberOfBedrooms = 3,
    //                PropertyType = PropertyType.Flat,
    //                ImageUrl = "propertyWide.png",
    //                NumberOfLikes = 66,
    //                ForSale = false
    //            },
    //            new PropertyDTO
    //            {
    //                Id = 4,
    //                Title = "3 Bedroom Independent House",
    //                Address = "132, West street, New York, United States",
    //                City = "New York",
    //                Price = 40500,
    //                SquareMeters = 1800,
    //                NumberOfBedrooms = 3,
    //                PropertyType = PropertyType.House,
    //                ImageUrl = "propertyWide.png",
    //                NumberOfLikes = 14,
    //                ForSale = true
    //            },
    //            new PropertyDTO
    //            {
    //                Id = 5,
    //                Title = "3 Bedroom Independent House",
    //                Address = "132, West street, New York, United States",
    //                City = "New York",
    //                Price = 40500,
    //                SquareMeters = 1000,
    //                NumberOfBedrooms = 3,
    //                PropertyType = PropertyType.House,
    //                ImageUrl = "propertyWide.png",
    //                NumberOfLikes = 2,
    //                ForSale = true
    //            },
    //        };

    //        return Task.FromResult(popular.AsEnumerable());
    //    }
    //}
}
