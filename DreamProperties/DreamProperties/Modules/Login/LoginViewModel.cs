using System;
using System.Threading.Tasks;
using DreamProperties.Common.Base;
using DreamProperties.Common.Navigation;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Essentials;

namespace DreamProperties.Modules.Login
{
    public class LoginViewModel: BaseViewModel
    {
        private const string AUTHENTICATION_URL = "https://dreamproperties.azurewebsites.net/api/auth/";

        private readonly INavigationService _navigationService;

        public LoginViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
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
            //simpler way
            
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

                //string authToken = result.AccessToken;
                //string refreshToken = result.RefreshToken;
                //var jwtTokenExpiresIn = result.Properties["jwt_token_expires"];

                //var userInfo = new Dictionary<string, string>
                //{
                //    { "token", authToken },
                //    { "name", $"{result.Properties["firstName"]} {result.Properties["secondName"]}"},
                //};

                //TODO navigate to home view
                _navigationService.GoToMainFlow();
            }
            catch (TaskCanceledException)
            {
                //User canceled auth flow;
            }
        }
    }
}
