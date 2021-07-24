using DreamProperties.API.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DreamProperties.API.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IConfiguration _configuration;

        public AuthService(UserManager<AppUser> userManager,
                           SignInManager<AppUser> signInManager,
                           IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        public async Task<AppUser> SignInUser(AuthenticateResult auth)
        {
            AppUser appUser = GenerateUserFromClaims(auth);

            await CreateAndSignInUser(appUser);

            return appUser;
        }

        private static AppUser GenerateUserFromClaims(AuthenticateResult auth)
        {
            var claims = auth.Principal.Identities.FirstOrDefault()?.Claims;

            var email = string.Empty;
            email = claims?.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            var givenName = claims?.FirstOrDefault(c => c.Type == ClaimTypes.GivenName)?.Value;
            var lastName = claims?.FirstOrDefault(c => c.Type == ClaimTypes.Surname)?.Value;
            var nameIdentifier = claims?.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            var appUser = new AppUser
            {
                Email = email,
                FirstName = givenName,
                LastName = lastName,
            };
            return appUser;
        }

        private async Task CreateAndSignInUser(AppUser appUser)
        {
            var user = await _userManager.FindByEmailAsync(appUser.Email);

            if (user == null)
            {
                await RegisterUser(appUser);
                user = appUser;
            }

            await _signInManager.SignInAsync(user, true);
        }

        public async Task<IdentityResult> RegisterUser(AppUser appUser)
        {
            appUser.UserName = appUser.Email;
            return await _userManager.CreateAsync(appUser);
        }

        public (string token, double expirySeconds) GenerateJwtToken(AppUser user)
        {
            var issuedAt = DateTimeOffset.UtcNow;

            //Learn more about JWT claims at: https://tools.ietf.org/html/rfc7519#section-4
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()), //Subject, should be unique in this scope
                new Claim(JwtRegisteredClaimNames.Iat, //Issued at, when the token is issued
                    issuedAt.ToUnixTimeMilliseconds().ToString(), ClaimValueTypes.Integer64),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), //Unique identifier for this specific token

                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = issuedAt.AddMinutes(Convert.ToDouble(_configuration["Jwt:Expiration"]));
            var expirySeconds = (long)TimeSpan.FromSeconds(Convert.ToDouble(_configuration["Jwt:Expiration"])).TotalSeconds;

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Issuer"],
                claims,
                expires: expires.DateTime,
                signingCredentials: creds
            );

            return (new JwtSecurityTokenHandler().WriteToken(token), expirySeconds);
        }
    }
}
