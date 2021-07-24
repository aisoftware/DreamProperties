using DreamProperties.API.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DreamProperties.API.Services
{
    public interface IAuthService
    {
        Task<AppUser> SignInUser(AuthenticateResult auth);
        (string token, double expirySeconds) GenerateJwtToken(AppUser user);
        Task<IdentityResult> RegisterUser(AppUser appUser);
    }
}
