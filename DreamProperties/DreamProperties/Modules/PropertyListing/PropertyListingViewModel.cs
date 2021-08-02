using DreamProperties.Common.Base;
using DreamProperties.Common.Controllers;
using DreamProperties.Common.Models;
using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DreamProperties.Modules.PropertyListing
{
    [QueryProperty("Parameter", "parameter")]
    public class PropertyListingViewModel : BaseViewModel
    {

        private IPropertyController _propertyController;

        private ObservableCollection<PropertyDTO> _properties;

        public PropertyListingViewModel(IPropertyController propertyController)
        {
            _propertyController = propertyController;
            _properties = new ObservableCollection<PropertyDTO>();
        }

        public async Task InitializeAsync()
        {
            var properties = await _propertyController.GetAllProperties();
            Properties = new ObservableCollection<PropertyDTO>(properties);
        }

        private string _parameter;
        public string Parameter
        {
            get => _parameter;
            set
            {
                _parameter = Uri.UnescapeDataString(value);
                _parameter = JsonConvert.DeserializeObject<string>(_parameter);
            }
        }

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
