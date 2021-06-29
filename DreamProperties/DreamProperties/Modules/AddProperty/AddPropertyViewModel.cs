using DreamProperties.Common.Base;
using System;
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

        public AddPropertyViewModel()
        {
            TypeSelection = "House/Villa";
        }

        public Command<string> TypeCommand { get => new Command<string>(SelectType); }

        public Command<string> AmenityCommand { get => new Command<string>(SelectAmenity); }

        public AsyncCommand GetLocationCommand { get => new AsyncCommand(GetLocation); }

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

        private string _selectedAmenities;
        public string SelectedAmenities { get => _selectedAmenities; set => SetProperty(ref _selectedAmenities, value); }

        private string _typeSelection;
        public string TypeSelection
        {
            get => _typeSelection;
            set
            {
                string newValue = $"Currently selected: {value}";
                SetProperty(ref _typeSelection, newValue);
            }
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
            throw new NotImplementedException();
        }
    }
}
