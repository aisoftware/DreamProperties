﻿using DreamProperties.Common.Base;
using DreamProperties.Common.Controllers;
using DreamProperties.Common.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace DreamProperties.Modules.Favorites
{
    public class FavoritesViewModel: BaseViewModel
    {

        private IPropertyController _propertyController;

        private ObservableCollection<Property> _properties;

        public FavoritesViewModel(IPropertyController propertyController)
        {
            _propertyController = propertyController;
            _properties = new ObservableCollection<Property>();
        }

        public override async Task InitializeAsync(object parameter)
        {
            var favorites = await _propertyController.GetPopularProperties();
            Properties = new ObservableCollection<Property>(favorites);
        }

        public ObservableCollection<Property> Properties
        {
            get => _properties;
            set
            {
                SetProperty(ref _properties, value);
            }
        }
    }
}
