using DreamProperties.Common.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DreamProperties.Common.Controllers
{
    public interface IPropertyController
    {
        Task<IEnumerable<Property>> GetFavoriteProperties();
    }

    public class FakePropertyController : IPropertyController
    {
        public Task<IEnumerable<Property>> GetFavoriteProperties()
        {
            var favorites = new List<Property>
            {
                new Property("New York", 3, 1800,PropertyType.Flat, 88, false),
                new Property("Los Angeles", 2, 1600,PropertyType.Flat, 54, false),
                new Property("New York", 2, 50000,PropertyType.House, 33),
                                new Property("Los Angeles", 3, 2000,PropertyType.Flat, 4, false),
                new Property("New York", 3, 80000,PropertyType.House, 1),
            };

            return Task.FromResult(favorites.AsEnumerable());
        }
    }
}
