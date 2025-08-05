using Microsoft.AspNetCore.Mvc;
using SpotifyAPI.Web;
using System.Security.Claims;
using MisticFy.Services;
using Microsoft.AspNetCore.Authorization;

namespace MisticFy.src.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController(IConfiguration configuration, IUserService _userService, ITokenService _token) : ControllerBase
    {
        private readonly string _clientId = configuration["Spotify:ClientId"];
        private readonly string _clientSecret = configuration["Spotify:ClientSecret"];
        private readonly string _redirectUri = configuration["Spotify:RedirectUri"];

        [HttpGet("authorize")]
        public IActionResult Authorize()
        {
            LoginRequest request = new(new Uri(_redirectUri), _clientId, LoginRequest.ResponseType.Code)
            {
                Scope = [Scopes.UserReadPrivate, Scopes.UserReadEmail, Scopes.PlaylistModifyPublic, Scopes.PlaylistModifyPrivate]
            };
            var uri = request.ToUri();
            return Redirect(uri.ToString());
        }

        [HttpGet("callback")]
        public async Task<IActionResult> Callback(string code)
        {
            try
            {
                var tokenResponse = await new OAuthClient().RequestToken(
                    new AuthorizationCodeTokenRequest(_clientId, _clientSecret, code, new Uri(_redirectUri))
                );

                var spotify = new SpotifyClient(tokenResponse.AccessToken);
                var spotifyProfile = await spotify.UserProfile.Current();

                var user = await _userService.FindOrCreateUserAsync(
                    spotifyProfile.Id,
                    spotifyProfile.DisplayName,
                    spotifyProfile.Email,
                    tokenResponse.AccessToken,
                    tokenResponse.RefreshToken,
                    tokenResponse.ExpiresIn
                );

                var claims = new List<Claim>
            {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Email, user.Email)
            };

                var jwtToken = _token.GenerateAccessToken(claims, configuration);

                Response.Cookies.Append("SessionToken", jwtToken, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = false, 
                    SameSite = SameSiteMode.Lax,
                    Expires = DateTimeOffset.UtcNow.AddHours(1)
                });

                return Ok(new { message = $"User {user.Name} logged in." });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}