using DreamProperties.Common.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DreamProperties.Common.Controllers
{
    public interface IPropertyController
    {
        Task<IEnumerable<Property>> GetPopularProperties();
    }

    public class FakePropertyController : IPropertyController
    {
        public Task<IEnumerable<Property>> GetPopularProperties()
        {
            var popular = new List<Property>
            {
                new Property("3 Bedroom Independent House", "132, West street, New York, United States", "New York", 40500, 1800, 3,PropertyType.House, "propertyWide.png", 88),
                new Property("2 Bedroom Flat", "132, West street, New York, United States", "Los Angeles", 1800, 50, 2,PropertyType.Flat, "propertyWide.png", 75, false),
                new Property("3 Bedroom Flat", "132, West street, New York, United States", "New York", 2200, 60, 3,PropertyType.Flat, "propertyWide.png", 66, false),
                new Property("3 Bedroom Independent House", "132, West street, New York, United States", "New York", 40500, 1800, 3,PropertyType.House, "propertyWide.png", 14),
                new Property("3 Bedroom Independent House", "132, West street, New York, United States", "New York", 40500, 1800, 3,PropertyType.House, "propertyWide.png", 2),
            };

            return Task.FromResult(popular.AsEnumerable());
        }
    }
}
