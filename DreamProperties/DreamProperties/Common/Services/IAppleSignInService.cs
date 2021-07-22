using System;
using DreamProperties.Common.Models;
using System.Threading.Tasks;

namespace DreamProperties.Common.Services
{
    //based on https://www.xamboy.com/2020/01/13/sign-in-with-apple-in-xamarin-forms/
    public interface IAppleSignInService
    {
        Task<AppleSignInCredentialState> GetCredentialStateAsync(string userId);
        Task<AppleAccount> SignInAsync();
    }
}
