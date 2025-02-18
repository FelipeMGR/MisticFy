using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpotifyAPI.Web;

namespace MisticFy.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        [HttpGet("{login}")]
        public IActionResult Login(string login)
        {
            var loginRequest = new LoginRequest(
              new Uri("http://localhost:5543"),
              "ClientId",
              LoginRequest.ResponseType.Code
            )
            {
                Scope = new[] { Scopes.PlaylistReadPrivate, Scopes.PlaylistReadCollaborative }
            };
            var uri = loginRequest.ToUri();

            return Redirect(uri.ToString());
        }
    }
}
