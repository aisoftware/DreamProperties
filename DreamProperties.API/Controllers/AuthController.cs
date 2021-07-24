using AutoMapper;
using DreamProperties.API.Models;
using DreamProperties.Common.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DreamProperties.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private const string MOBILEAPP_SCHEME = "xamdream";

        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AuthController> _logger;
        private readonly IMapper _mapper;

        public AuthController(UserManager<AppUser> userManager,
                              SignInManager<AppUser> signInManager,
                              IConfiguration configuration,
                              ILogger<AuthController> logger,
                              IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Register([FromBody] UserDTO UserDto)
        {
            _logger.LogInformation($"Registration attempt for {UserDto.Email}");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var user = _mapper.Map<AppUser>(UserDto);
                user.UserName = user.Email;
                var result = await _userManager.CreateAsync(user);

                if (!result.Succeeded)
                {
                    return BadRequest("Error while creating a new user");
                }

                return Ok();
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

            //Move to auth service
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
                //move to auth service
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

                await CreateOrGetUser(appUser);
                //var authToken = GenerateJwtToken(appUser);

                // Get parameters to send back to the callback
                //var qs = new Dictionary<string, string>
                //{
                //    { "access_token", authToken.token },
                //    { "refresh_token",  string.Empty },
                //    { "jwt_token_expires", authToken.expirySeconds.ToString() },
                //    { "email", email },
                //    { "firstName", givenName },
                //    { "secondName", surName },
                //};

                // Build the result url
                //var url = MOBILEAPP_SCHEME + "://#" + string.Join(
                //    "&",
                //    qs.Where(kvp => !string.IsNullOrEmpty(kvp.Value) && kvp.Value != "-1")
                //    .Select(kvp => $"{WebUtility.UrlEncode(kvp.Key)}={WebUtility.UrlEncode(kvp.Value)}"));
                //// Redirect to final url
                Request.HttpContext.Response.Redirect(MOBILEAPP_SCHEME + "://");
            }
        }

        //move to auth service
        private async Task CreateOrGetUser(AppUser appUser)
        {
            var user = await _userManager.FindByEmailAsync(appUser.Email);

            if (user == null)
            {
                appUser.UserName = appUser.Email;
                await _userManager.CreateAsync(appUser);
                user = appUser;
            }

            await _signInManager.SignInAsync(user, true);
        }

        //private (string token, double expirySeconds) GenerateJwtToken(AppUser user)
        //{
        //    var issuedAt = DateTimeOffset.UtcNow;

        //    //Learn more about JWT claims at: https://tools.ietf.org/html/rfc7519#section-4
        //    var claims = new List<Claim>
        //    {
        //        new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()), //Subject, should be unique in this scope
        //        new Claim(JwtRegisteredClaimNames.Iat, //Issued at, when the token is issued
        //            issuedAt.ToUnixTimeMilliseconds().ToString(), ClaimValueTypes.Integer64),
        //        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), //Unique identifier for this specific token

        //        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        //        new Claim(ClaimTypes.Email, user.Email)
        //    };

        //    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtKey"]));
        //    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        //    var expires = issuedAt.AddMinutes(Convert.ToDouble(_configuration["JwtExpire"]));
        //    var expirySeconds = (long)TimeSpan.FromSeconds(Convert.ToDouble(_configuration["JwtExpire"])).TotalSeconds;

        //    var token = new JwtSecurityToken(
        //        _configuration["JwtIssuer"],
        //        _configuration["JwtIssuer"],
        //        claims,
        //        expires: expires.DateTime,
        //        signingCredentials: creds
        //    );

        //    return (new JwtSecurityTokenHandler().WriteToken(token), expirySeconds);
        //}
    }
}
