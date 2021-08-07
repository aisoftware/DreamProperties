using DreamProperties.Common;
using DreamProperties.Common.Base;
using DreamProperties.Common.Controllers;
using DreamProperties.Common.Models;
using DreamProperties.Common.Navigation;
using DreamProperties.Common.Network;
using DreamProperties.Modules.AddProperty;
using DreamProperties.Modules.PropertyListing;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.ObjectModel;

namespace DreamProperties.Modules.Home
{
    public class HomeViewModel: BaseViewModel
    {
        private const string PROPERTY_ENDPOINT = "property";
        private readonly INavigationService _navigationService;
        private readonly INetworkService _networkService;

        public HomeViewModel(INavigationService navigationService,
                             INetworkService networkService)
        {
            _navigationService = navigationService;
            _networkService = networkService;
            _popularProperties = new ObservableCollection<PropertyDTO>();
        }

        public async Task InitializeAsync()
        {
            try
            {
                IsBusy = true;
                var favorites = (await _networkService.GetAsync<List<PropertyDTO>>(Constants.API_URL + PROPERTY_ENDPOINT))
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
        public AsyncCommand SearchByCityCommand { get => new AsyncCommand(SearchByCity); }

        public AsyncCommand AddPropertyCommand { get => new AsyncCommand(AddProperty); }

        public AsyncCommand RefreshCommand { get => new AsyncCommand(InitializeAsync, () => IsNotBusy); }

        private ObservableCollection<PropertyDTO> _popularProperties;
        public ObservableCollection<PropertyDTO> PopularProperties 
        {
            get => _popularProperties;
            set => SetProperty(ref _popularProperties, value);
        }

        public string EnteredCity { get; set; }

        private async Task AddProperty()
        {
            await _navigationService.PushAsync<AddPropertyViewModel>();
        }

        private async Task PerformSearch(string propertyType)
        {
            await _navigationService
                .PushAsync<PropertyListingViewModel,SearchQuery>(new SearchQuery 
                { 
                    SearchType = SearchType.PropertyType,
                    Term = propertyType
                });
        }

        private async Task SearchByCity()
        {
            await _navigationService
                .PushAsync<PropertyListingViewModel, SearchQuery>(new SearchQuery
                {
                    SearchType = SearchType.City,
                    Term = EnteredCity
                });
        }
    }
}
