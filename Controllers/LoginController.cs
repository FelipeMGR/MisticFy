using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using SpotifyAPI.Web;
using SpotifyAPI.Web.Auth;
using MisticFy.Models;
using Microsoft.OpenApi.Writers;
using System.Security.Claims;
using MisticFy.Services;

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
        var request = new LoginRequest(new Uri(_redirectUri), _clientId, LoginRequest.ResponseType.Code)
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
            new(ClaimTypes.Email, user.Email),
            new(ClaimTypes.AuthorizationDecision, user.AccessToken)
            };

            var jwtToken = _token.GenerateAccessToken(claims, configuration);
            Console.WriteLine($"Logged in as: {spotifyProfile.DisplayName}");
            return Ok(new { Token = jwtToken });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Error = ex.Message });
        }
    }
}