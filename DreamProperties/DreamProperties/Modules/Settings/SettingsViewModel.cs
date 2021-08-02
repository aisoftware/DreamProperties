using DreamProperties.Common.Base;
using DreamProperties.Common.Navigation;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace DreamProperties.Modules.Settings
{
    public class SettingsViewModel : BaseViewModel
    {
        private string _name;
        private string _email;

        private readonly INavigationService _navigationService;

        public SettingsViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public string Name { get => _name; set => SetProperty(ref _name, value); }
        public string Email { get => _email; set => SetProperty(ref _email, value); }

        public Command LogoutCommand { get => new Command(PerformLogout); }

        public async Task InitializeAsync()
        {
            Name = await SecureStorage.GetAsync("name");
            Email = await SecureStorage.GetAsync("email");
        }

        private void PerformLogout()
        {
            Preferences.Remove("logged");
            SecureStorage.Remove("token");
            SecureStorage.Remove("name");
            SecureStorage.Remove("email");

            _navigationService.GoToLoginFlow();
        }
    }
}
