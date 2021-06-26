using DreamProperties.Common.Base;
using DreamProperties.Common.Controllers;
using DreamProperties.Common.Models;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.ObjectModel;

namespace DreamProperties.Modules.Home
{
    public class HomeViewModel: BaseViewModel
    {
        private readonly IPropertyController _propertyController;

        public HomeViewModel(IPropertyController propertyController)
        {
            _propertyController = propertyController;
            _popularProperties = new ObservableCollection<Property>();
        }

        public override async Task InitializeAsync(object parameter)
        {
            var favorites = await _propertyController.GetFavoriteProperties();
            PopularProperties = new ObservableCollection<Property>(favorites);
        }

        public AsyncCommand<string> SearchCommand { get => new AsyncCommand<string>(PerformSearch); }

        public AsyncCommand AddPropertyCommand { get => new AsyncCommand(AddProperty); }

        private ObservableCollection<Property> _popularProperties;
        public ObservableCollection<Property> PopularProperties 
        {
            get => _popularProperties;
            set => SetProperty(ref _popularProperties, value);
        }

        private Task AddProperty()
        {
            return Task.CompletedTask;
        }

        private Task PerformSearch(string propertyType)
        {
            return Task.CompletedTask;
        }
    }
}
