using DreamProperties.Common;
using DreamProperties.Common.Base;
using DreamProperties.Common.Controllers;
using DreamProperties.Common.Models;
using DreamProperties.Common.Navigation;
using DreamProperties.Common.Network;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace DreamProperties.Modules.AddProperty
{
    public class AddPropertyViewModel : BaseViewModel
    {
        private const string PROPERTY_ENDPOINT = "property";
        private string _propertyType = string.Empty;
        private List<string> _amenities = new List<string>();
        private string _city = string.Empty;
        private FileResult _fileResult = null;

        private readonly INavigationService _navigationService;
        private readonly INetworkService _networkService;

        public AddPropertyViewModel()
        {
            TypeSelection = "House";
        }

        public AddPropertyViewModel(INavigationService navigationService,
            INetworkService networkService) : this()
        {
            _navigationService = navigationService;
            _networkService = networkService;
        }

        public Command<string> TypeCommand { get => new Command<string>(SelectType); }

        public Command<string> AmenityCommand { get => new Command<string>(SelectAmenity); }

        public AsyncCommand GetLocationCommand { get => new AsyncCommand(GetLocation); }

        public AsyncCommand CreatePropertyCommand { get => new AsyncCommand(CreateProperty); }

        public AsyncCommand ChooseImageCommand { get => new AsyncCommand(ChooseImage); }

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

        private string _title;
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
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

        public int SquareMeters { get; set; }

        private string _selectedImage;
        public string SelectedImage { get => _selectedImage; set => SetProperty(ref _selectedImage, value); }

        private async Task CreateProperty()
        {
            Enum.TryParse(TypeSelection, out PropertyType propertyType);
            var createdProperty = new CreatePropertyDTO
            {
                Address = Address,
                Amenities = SelectedAmenities,
                City = _city,
                ForSale = true,
                NumberOfBedrooms = (int)NumberOfBedrooms,
                Price = (int)Price,
                PropertyType = propertyType,
                SquareMeters = SquareMeters,
                Title = Title
            };


            var newProperty = await _networkService.PostAsync<PropertyDTO>(Constants.API_URL + PROPERTY_ENDPOINT,
                                                             JsonConvert.SerializeObject(createdProperty));

            //upload image
            bool sucess = await _networkService.PostAsync($"{Constants.API_URL}image?property={newProperty.Id}", _fileResult);

            //dialog about sucess

            await _navigationService.GoBackAsync();
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
                _city = placemark.Locality;
                Address = $"{placemark.Locality}, {placemark.SubAdminArea}, {placemark.AdminArea}, {placemark.CountryName}";
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

        async Task ChooseImage()
        {
            try
            {
                var photo = await MediaPicker.PickPhotoAsync();
                if (photo == null)
                {
                    return;
                }
                // save the file into local storage
                var newFile = Path.Combine(FileSystem.CacheDirectory, photo.FileName);
                using (var stream = await photo.OpenReadAsync())
                using (var newStream = File.OpenWrite(newFile))
                    await stream.CopyToAsync(newStream);

                _fileResult = photo;

                SelectedImage = newFile;
            }
            catch (Exception ex)
            {
                //unable to get photo
            }
        }
    }
}
