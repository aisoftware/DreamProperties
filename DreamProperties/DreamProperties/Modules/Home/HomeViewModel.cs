﻿using DreamProperties.Common.Base;
using DreamProperties.Common.Controllers;
using DreamProperties.Common.Models;
using DreamProperties.Common.Navigation;
using DreamProperties.Modules.AddProperty;
using DreamProperties.Modules.PropertyListing;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.ObjectModel;

namespace DreamProperties.Modules.Home
{
    public class HomeViewModel: BaseViewModel
    {
        private readonly IPropertyController _propertyController;
        private readonly INavigationService _navigationService;

        public HomeViewModel(IPropertyController propertyController,
                             INavigationService navigationService)
        {
            _propertyController = propertyController;
            _navigationService = navigationService;
            _popularProperties = new ObservableCollection<PropertyDTO>();
        }

        public async Task InitializeAsync()
        {
            try
            {
                IsBusy = true;
                var favorites = (await _propertyController.GetAllProperties())
                    .OrderByDescending(x => x.NumberOfLikes)
                    .Take(5);
                PopularProperties = new ObservableCollection<PropertyDTO>(favorites);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public AsyncCommand<string> SearchCommand { get => new AsyncCommand<string>(PerformSearch); }

        public AsyncCommand AddPropertyCommand { get => new AsyncCommand(AddProperty); }

        public AsyncCommand RefreshCommand { get => new AsyncCommand(InitializeAsync, () => IsNotBusy); }

        private ObservableCollection<PropertyDTO> _popularProperties;
        public ObservableCollection<PropertyDTO> PopularProperties 
        {
            get => _popularProperties;
            set => SetProperty(ref _popularProperties, value);
        }

        private async Task AddProperty()
        {
            await _navigationService.PushAsync<AddPropertyViewModel>();
        }

        private async Task PerformSearch(string propertyType)
        {
            await _navigationService.PushAsync<PropertyListingViewModel,string>(propertyType);
        }
    }
}
