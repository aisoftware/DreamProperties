using DreamProperties.Common.Base;
using DreamProperties.Common.Controllers;
using DreamProperties.Common.Models;
using DreamProperties.Common.Navigation;
using DreamProperties.Modules.AddProperty;
using System;
using System.Collections.ObjectModel;
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

        public override async Task InitializeAsync(object parameter)
        {
            var favorites = await _propertyController.GetPopularProperties();
            PopularProperties = new ObservableCollection<PropertyDTO>(favorites);
        }

        public AsyncCommand<string> SearchCommand { get => new AsyncCommand<string>(PerformSearch); }

        public AsyncCommand AddPropertyCommand { get => new AsyncCommand(AddProperty); }

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

        private Task PerformSearch(string propertyType)
        {
            return Task.CompletedTask;
        }
    }
}
