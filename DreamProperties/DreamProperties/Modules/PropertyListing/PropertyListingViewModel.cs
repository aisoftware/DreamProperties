using DreamProperties.Common;
using DreamProperties.Common.Base;
using DreamProperties.Common.Controllers;
using DreamProperties.Common.Models;
using DreamProperties.Common.Network;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DreamProperties.Modules.PropertyListing
{
    [QueryProperty("Parameter", "parameter")]
    public class PropertyListingViewModel : BaseViewModel
    {
        private const string PROPERTY_ENDPOINT = "property";
        private readonly INetworkService _networkService;

        private ObservableCollection<PropertyDTO> _properties;

        public PropertyListingViewModel(INetworkService networkService)
        {
            _networkService = networkService;
            _properties = new ObservableCollection<PropertyDTO>();
        }

        public async Task InitializeAsync()
        {

            IEnumerable<PropertyDTO> properties = null;
            if (SearchQuery == null)
            {
                properties = await _networkService.GetAsync<List<PropertyDTO>>(Constants.API_URL + PROPERTY_ENDPOINT);
            }
            else
            {
                string parameter = string.Empty;

                if (SearchQuery.SearchType == SearchType.City)
                {
                    parameter = $"city={Uri.EscapeDataString(SearchQuery.Term)}";
                }
                else
                {
                    parameter = $"type={SearchQuery.Term}";
                }

                properties = await _networkService.GetAsync<List<PropertyDTO>>(Constants.API_URL + $"property/query?{parameter}");
            }
            Properties = new ObservableCollection<PropertyDTO>(properties);
        }

        private string _parameter;
        public string Parameter
        {
            get => _parameter;
            set
            {
                _parameter = Uri.UnescapeDataString(value);
                SearchQuery = JsonConvert.DeserializeObject<SearchQuery>(_parameter);
            }
        }

        public SearchQuery SearchQuery { get; set; }

        public ObservableCollection<PropertyDTO> Properties 
        { 
            get => _properties;
            set
            {
                SetProperty(ref _properties, value);
            }
        }
    }
}
