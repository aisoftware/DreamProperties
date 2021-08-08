using System;
using System.Threading.Tasks;
using DreamProperties.Common.Base;
using DreamProperties.Common.Navigation;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Essentials;
using System.Web;
using DreamProperties.Common.Network;
using DreamProperties.Common.Models;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace DreamProperties.Modules.Login
{
    public class LoginViewModel: BaseViewModel
    {
        private const string AUTHENTICATION_URL = "https://dreamproperties.azurewebsites.net/api/auth/";

        private readonly INavigationService _navigationService;
        private readonly INetworkService _networkService;

        public LoginViewModel(INavigationService navigationService,
                              INetworkService networkService)
        {
            _navigationService = navigationService;
            _networkService = networkService;
        }

        public AsyncCommand FacebookAuthCommand { get => new AsyncCommand(FacebookAuthenticate); }
        public AsyncCommand GoogleAuthCommand { get => new AsyncCommand(GoogleAuthenticate); }
        public AsyncCommand AppleAuthCommand { get => new AsyncCommand(AppleAuthenticate); }

        private Task FacebookAuthenticate()
        {
            return PerformAuthentication("Facebook");
        }

        private Task GoogleAuthenticate()
        {
            return PerformAuthentication("Google");
        }

        private async Task AppleAuthenticate()
        {
            // Use Native Apple Sign In API's
            var options = new AppleSignInAuthenticator.Options
            {
                IncludeEmailScope = true,
                IncludeFullNameScope = true,
            };
            var authResult = await AppleSignInAuthenticator.AuthenticateAsync(options);

            string AuthToken = string.Empty;
            if (authResult.Properties.TryGetValue("name", out var name) && !string.IsNullOrEmpty(name))
                AuthToken += $"Name: {name}{Environment.NewLine}";
            if (authResult.Properties.TryGetValue("email", out var email) && !string.IsNullOrEmpty(email))
                AuthToken += $"Email: {email}{Environment.NewLine}";
            AuthToken += authResult?.AccessToken ?? authResult?.IdToken;

            var createdUser = new UserDTO
            {
                Email = email,
                FirstName = name,
                LastName = name
            };

            var callbackValues = await _networkService
                .PostAsync<Dictionary<string,string>>(AUTHENTICATION_URL + "register", JsonConvert.SerializeObject(createdUser));

            await SaveUserData(callbackValues["access_token"], name, email);

            _navigationService.GoToMainFlow();
        }

        private async Task PerformAuthentication(string scheme)
        {
            try
            {
                var authUrl = new Uri(AUTHENTICATION_URL + scheme);
                var callbackUrl = new Uri("xamdream://");

                var result = await WebAuthenticator.AuthenticateAsync(authUrl, callbackUrl);

                var isResultOk = result != null;

                string authToken = result.AccessToken;
                string name = $"{result.Properties["firstName"]} {result.Properties["lastName"]}";
                string email = HttpUtility.UrlDecode(result.Properties["email"]);

                await SaveUserData(authToken, name, email);

                _navigationService.GoToMainFlow();
            }
            catch (TaskCanceledException)
            {
                //User canceled auth flow
            }
        }

        private static async Task SaveUserData(string authToken, string name, string email)
        {
            await SecureStorage.SetAsync("token", authToken);
            await SecureStorage.SetAsync("name", name);
            await SecureStorage.SetAsync("email", email);

            Preferences.Set("logged", true);
        }
    }
}
