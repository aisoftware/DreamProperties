using DreamProperties.Common.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace DreamProperties.Modules.AddProperty
{
    public class AddPropertyViewModel : BaseViewModel
    {
        private string _propertyType = string.Empty;
        private List<string> _amenities = new List<string>();

        public AddPropertyViewModel()
        {
            TypeSelection = "House/Villa";
        }

        public Command<string> TypeCommand { get => new Command<string>(SelectType); }

        public Command<string> AmenityCommand { get => new Command<string>(SelectAmenity); }

        public AsyncCommand GetLocationCommand { get => new AsyncCommand(GetLocation); }

        public AsyncCommand CreatePropertyCommand { get => new AsyncCommand(CreateProperty); }

        private string _address;
        public string Address
        {
            get => _address;
            set => SetProperty(ref _address, value);
        }

        private double _price = 1000;
        public double Price
        {
            get => _price;
            set
            {
                int newValue = (int)(value + 50) / 100 * 100;
                SetProperty(ref _price, (double)newValue);
            }
        }

        private string _description;
        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        private string _selectedAmenities;
        public string SelectedAmenities
        { 
            get => _selectedAmenities;
            set => SetProperty(ref _selectedAmenities, $"Selected: {value}");
        }

        private string _typeSelection;
        public string TypeSelection
        {
            get => _typeSelection;
            set => SetProperty(ref _typeSelection, $"Selected: {value}");
        }

        private double _numberOfBedrooms = 1;
        public double NumberOfBedrooms
        {
            get => _numberOfBedrooms;
            set => SetProperty(ref _numberOfBedrooms, value);
        }

        private Task CreateProperty()
        {
            return Task.CompletedTask;
        }

        private async Task GetLocation()
        {
            try
            {
                var location = await Geolocation.GetLastKnownLocationAsync();

                if (location == null)
                {
                    return;
                }

                var placemarks = await Geocoding.GetPlacemarksAsync(location.Latitude, location.Longitude);
                var placemark = placemarks?.FirstOrDefault();
                if (placemark == null)
                {
                    return;
                }
                Address = $"{placemark.Locality}, {placemark.SubAdminArea}, {placemark.AdminArea}, {placemark.CountryName}";
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Handle not supported on device exception
            }
            catch (FeatureNotEnabledException fneEx)
            {
                // Handle not enabled on device exception
            }
            catch (PermissionException pEx)
            {
                // Handle permission exception
            }
            catch (Exception ex)
            {
                // Unable to get location
            }
        }

        private void SelectType(string selectedType)
        {
            _propertyType = selectedType;
            TypeSelection = _propertyType;
        }


        private void SelectAmenity(string selected)
        {
            if (_amenities.Contains(selected))
            {
                _amenities.Remove(selected);
            }
            else
            {
                _amenities.Add(selected);
            }

            SelectedAmenities = String.Join(", ", _amenities);
        }
    }
}
