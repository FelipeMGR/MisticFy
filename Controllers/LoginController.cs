using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using SpotifyAPI.Web;
using SpotifyAPI.Web.Auth;
using MisticFy.Models;
using Microsoft.OpenApi.Writers;

[ApiController]
[Route("[controller]")]
public class LoginController(IConfiguration configuration) : ControllerBase
{
    private readonly string _clientId = configuration["Spotify:ClientId"];
    private readonly string _clientSecret = configuration["Spotify:ClientSecret"];
    private readonly string _redirectUri = configuration["Spotify:RedirectUri"];

    [HttpGet("authorize")]
    public IActionResult Authorize()
    {
        var request = new LoginRequest(new Uri(_redirectUri), _clientId, LoginRequest.ResponseType.Code)
        {
            Scope = new List<string> { Scopes.UserReadPrivate, Scopes.UserReadEmail }
        };
        var uri = request.ToUri();
        return Redirect(uri.ToString());
    }

    [HttpGet("callback")]
    public async Task<IActionResult> Callback(string code)
    {
        try
        {
            var config = SpotifyClientConfig.CreateDefault();
            var request = new AuthorizationCodeTokenRequest(
                _clientId,
                _clientSecret,
                code,
                new Uri(_redirectUri)
            );

            var response = await new OAuthClient(config).RequestToken(request);

            // Use the access token to initialize the Spotify client
            var spotify = new SpotifyClient(config.WithToken(response.AccessToken));

            // Fetch user profile (example)
            var profile = await spotify.UserProfile.Current();
            Console.WriteLine($"Logged in as: {profile.DisplayName}");

            return Ok(new { response.AccessToken, response.TokenType });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Error = ex.Message });
        }
    }
}