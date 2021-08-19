using DreamProperties.Common.Base;
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
        private readonly INavigationService _navigationService;
        private readonly IPropertyController _propertyController;

        public HomeViewModel(INavigationService navigationService,
                             IPropertyController propertyController)
        {
            _navigationService = navigationService;
            _propertyController = propertyController;
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
