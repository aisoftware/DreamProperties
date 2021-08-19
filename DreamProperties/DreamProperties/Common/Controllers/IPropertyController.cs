using DreamProperties.Common.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace DreamProperties.Common.Controllers
{
    public interface IPropertyController
    {
        Task<IEnumerable<PropertyDTO>> GetAllProperties();
        Task<IEnumerable<PropertyDTO>> GetProperties(SearchQuery searchQuery);
        Task<PropertyDTO> CreateProperty(CreatePropertyDTO propertyDTO);
        Task<bool> UploadImage(FileResult image, int propertyId);
    }
}
