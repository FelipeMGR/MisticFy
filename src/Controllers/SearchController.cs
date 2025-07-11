using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MisticFy.DTO;
using MisticFy.Services;
using SpotifyAPI.Web;

namespace MisticFy.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SearchController(ISpotifyService _service, IUserService _userService) : ControllerBase
    {
        [HttpGet]
        [Authorize]
        public async Task<ActionResult> Search([FromQuery] string query, [FromQuery] SearchRequest.Types types, [FromQuery] int limit = 10)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized("User not authenticated.");

            var user = await _userService.GetUserByIdAsync(int.Parse(userId));

            if (user == null || string.IsNullOrEmpty(user.AccessToken))
                return Unauthorized("User not found or access token missing.");

            var result = await _service.SearchAsync(user.AccessToken, query, types, limit);
            return Ok(result);
        }

    }
}
