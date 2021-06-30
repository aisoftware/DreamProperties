using DreamProperties.Common.Base;
using DreamProperties.Common.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Web;
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
            // Use Native Apple Sign In API's
            WebAuthenticatorResult r = await AppleSignInAuthenticator.AuthenticateAsync(
                new AppleSignInAuthenticator.Options()
                {
                    IncludeEmailScope = true,
                    IncludeFullNameScope = true,
                });
            //TODO navigate to home view
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
