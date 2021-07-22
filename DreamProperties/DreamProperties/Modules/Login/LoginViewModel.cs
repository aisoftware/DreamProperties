using DreamProperties.Common.Base;
using DreamProperties.Common.Navigation;
using DreamProperties.Common.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace DreamProperties.Modules.Login
{
    public class LoginViewModel: BaseViewModel
    {
        private const string AUTHENTICATION_URL = "https://dreamproperties.azurewebsites.net/api/auth/";

        private readonly INavigationService _navigationService;
        private readonly IAppleSignInService appleSignInService;

        public LoginViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            appleSignInService = DependencyService.Get<IAppleSignInService>();
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
            
            /*
            //based on https://www.xamboy.com/2020/01/13/sign-in-with-apple-in-xamarin-forms/
            var account = await appleSignInService.SignInAsync();
            if (account != null)
            {
                //Preferences.Set(App.LoggedInKey, true);
                //await SecureStorage.SetAsync(App.AppleUserIdKey, account.UserId);
                System.Diagnostics.Debug.WriteLine($"Signed in!\n  Name: {account?.Name ?? string.Empty}\n  Email: {account?.Email ?? string.Empty}\n  UserId: {account?.UserId ?? string.Empty}");
               // OnSignIn?.Invoke(this, default(EventArgs));
            }
            */
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
