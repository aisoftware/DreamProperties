using AutoMapper;
using DreamProperties.API.Models;
using DreamProperties.API.Services;
using DreamProperties.Common.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DreamProperties.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private const string MOBILEAPP_SCHEME = "xamdream";

        private readonly IConfiguration _configuration;
        private readonly ILogger<AuthController> _logger;
        private readonly IMapper _mapper;
        private readonly IAuthService _authService;

        public AuthController(IConfiguration configuration,
                              ILogger<AuthController> logger,
                              IMapper mapper,
                              IAuthService authService)
        {
            _configuration = configuration;
            _logger = logger;
            _mapper = mapper;
            _authService = authService;
        }

        [HttpPost]
        [Route("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Register([FromBody] UserDTO UserDto)
        {
            try
            {
                _logger.LogInformation($"Registration attempt for {UserDto.Email}");
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var user = _mapper.Map<AppUser>(UserDto);
                var result = await _authService.RegisterUser(user);

                if (!result.Succeeded)
                {
                    return BadRequest("Error while creating a new user");
                }

                var callbackValues = GetJwtCallbackValues(user, null);

                return Ok(callbackValues);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(Register)}");
                return StatusCode(500, "Internal server error. Please try again later.");
            }
        }

        [HttpGet("{scheme}")]
        public async Task SocialLogin([FromRoute] string scheme)
        {
            //NOTE: see https://docs.microsoft.com/en-us/xamarin/essentials/web-authenticator?tabs=android
            var auth = await Request.HttpContext.AuthenticateAsync(scheme);

            if (!auth.Succeeded
                || auth?.Principal == null
                || !auth.Principal.Identities.Any(id => id.IsAuthenticated)
                || string.IsNullOrEmpty(auth.Properties.GetTokenValue("access_token")))
            {
                // Not authenticated, challenge
                await Request.HttpContext.ChallengeAsync(scheme);
            }
            else
            {
                var appUser = await _authService.SignInUser(auth);
                Dictionary<string, string> qs = GetJwtCallbackValues(appUser, auth.Properties.GetTokenValue("refresh_token"));

                // Build the result url
                var finalUrl = MOBILEAPP_SCHEME + "://#" + string.Join(
                    "&",
                    qs.Where(kvp => !string.IsNullOrEmpty(kvp.Value) && kvp.Value != "-1")
                    .Select(kvp => $"{WebUtility.UrlEncode(kvp.Key)}={WebUtility.UrlEncode(kvp.Value)}"));

                Request.HttpContext.Response.Redirect(finalUrl);
            }
        }

        private Dictionary<string, string> GetJwtCallbackValues(AppUser appUser, string refreshToken)
        {
            var authToken = _authService.GenerateJwtToken(appUser);

            // Get parameters to send back to the callback
            var qs = new Dictionary<string, string>
                {
                    { "access_token", authToken.token },
                    { "refresh_token",  refreshToken ?? string.Empty },
                    { "jwt_token_expires", authToken.expirySeconds.ToString() },
                    { "email", appUser.Email },
                    { "firstName", appUser.FirstName },
                    { "lastName", appUser.LastName },
                };
            return qs;
        }
    }
}
