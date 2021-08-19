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
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;
using Xamarin.Essentials;
using DreamProperties.Common.Dialog;

namespace DreamProperties.Modules.PropertyListing
{
    [QueryProperty("Parameter", "parameter")]
    public class PropertyListingViewModel : BaseViewModel
    {
        private readonly IDialogMessage _dialogMessage;
        private readonly IPropertyController _propertyController;
        private readonly INetworkService _networkService;

        private ObservableCollection<PropertyDTO> _properties;

        public PropertyListingViewModel(IPropertyController propertyController,
                                        INetworkService networkService,
                                        IDialogMessage dialogMessage)
        {
            _propertyController = propertyController;
            _networkService = networkService;
            _dialogMessage = dialogMessage;
            _properties = new ObservableCollection<PropertyDTO>();
        }

        public async Task InitializeAsync()
        {
            IEnumerable<PropertyDTO> properties = null;
            if (SearchQuery == null)
            {
                properties = await _propertyController.GetAllProperties();
            }
            else
            {
                properties = await _propertyController.GetProperties(SearchQuery);
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
            set => SetProperty(ref _properties, value);
        }

        public AsyncCommand<int> LikeCommand { get => new AsyncCommand<int>(LikeProperty, () => IsNotBusy); }

        public AsyncCommand<PropertyDTO> ContactCommand { get => new AsyncCommand<PropertyDTO>(ContactPropertyOwner, () => IsNotBusy); }

        private async Task ContactPropertyOwner(PropertyDTO propertyDTO)
        {
            try
            {
                IsBusy = true;
                var mail = new MailDTO
                {
                    FromEmail = await SecureStorage.GetAsync("email"),
                    ToEmail = propertyDTO.OwnersEmail,
                    PropertyId = propertyDTO.Id,
                    PropertyTitle = propertyDTO.Title
                };

                await _networkService.PostAsync($"{Constants.API_URL}email/send", JsonConvert.SerializeObject(mail));
                await _dialogMessage.DisplayOkAlert("Success", "The email has been sent to the owner of this property.");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task LikeProperty(int propertyId)
        {
            try
            {
                IsBusy = true;
                await _networkService.PutAsync($"{Constants.API_URL}like/{propertyId}");
                await _dialogMessage.DisplayOkAlert("Success", "You have liked this property.");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
