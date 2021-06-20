using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Essentials;

namespace DreamProperties.Modules.Login
{
    public class LoginViewModel
    {
        private const string AUTHENTICATION_URL = "https://https://dreamproperties.azurewebsites.net/auth/";

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

        private Task AppleAuthenticate()
        {
            return PerformAuthentication("Apple");
        }

        private async Task PerformAuthentication(string scheme)
        {
            try
            {
                var authUrl = new Uri(AUTHENTICATION_URL + scheme);
                var callbackUrl = new Uri("xamdream://");

                var result = await WebAuthenticator.AuthenticateAsync(authUrl, callbackUrl);

                string authToken = result.AccessToken;
                string refreshToken = result.RefreshToken;
                var jwtTokenExpiresIn = result.Properties["jwt_token_expires"];

                var userInfo = new Dictionary<string, string>
                {
                    { "token", authToken },
                    { "name", $"{result.Properties["firstName"]} {result.Properties["secondName"]}"},
                };
            }
            catch (TaskCanceledException)
            {
                //User canceled auth flow;
            }
        }
    }
}
