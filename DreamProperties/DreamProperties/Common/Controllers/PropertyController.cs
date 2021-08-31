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
    public class PropertyController : IPropertyController
    {
        private const string PROPERTY_ENDPOINT = "property";
        private INetworkService _networkService;

        public PropertyController(INetworkService networkService)
        {
            _networkService = networkService;
        }

        public async Task<PropertyDTO> CreateProperty(CreatePropertyDTO propertyDTO)
        {
            var result = await _networkService.PostAsync<PropertyDTO>(Constants.API_URL + PROPERTY_ENDPOINT,
                                                             JsonConvert.SerializeObject(propertyDTO));
            return result;
        }

        public async Task<IEnumerable<PropertyDTO>> GetAllProperties()
        {
            var properties = await _networkService.GetAsync<List<PropertyDTO>>(Constants.API_URL + PROPERTY_ENDPOINT);

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

        public async Task<bool> UploadImage(FileResult image, int propertyId)
        {
            try
            {
                var result = await _networkService.PostAsync($"{Constants.API_URL}image?property={propertyId}", image);
                return result;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
