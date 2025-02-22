using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MisticFy.Models;
using SpotifyAPI.Web;

namespace MisticFy.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PlaylistController : ControllerBase
    {
        [HttpGet("{playlistId}")]
        public async Task<ActionResult> GetUserPlaylistAsyc([FromHeader(Name = "Authentication")] string token, [FromRoute] string playlistId)
        {

            var acessToken = token.Replace("Bearer ", "").Trim();

            if (string.IsNullOrEmpty(playlistId))
            {
                return BadRequest("Playlist ID is required.");
            }

            var config = SpotifyClientConfig.CreateDefault().WithToken(acessToken);
            var client = new SpotifyClient(config);

            var playlist = await client.Playlists.Get(playlistId);

            List<Music> playlistMusics = new List<Music>();

            foreach (var item in playlist.Tracks.Items)
            {
                var track = item.Track as FullTrack;
                if (track != null)
                {
                    playlistMusics.Add(new Music
                    {
                        MusicName = track.Name
                    });
                }
            }
            return Ok(new Playlist
            {
                Name = playlist.Name,
                Description = playlist.Description,
                IsPublic = playlist.Public,
                Musics = playlistMusics
            });
        }

        [HttpPost("createPlaylist")]
        public async Task<ActionResult> CreatePlaylistAsync([FromBody] Playlist playlist, [FromHeader(Name = "Authentication")] string token)
        {
            var acessToken = token.Replace("Bearer ", "");

            var config = SpotifyClientConfig.CreateDefault();
            var spotify = new SpotifyClient(config.WithToken(acessToken));

            var currentUser = await spotify.UserProfile.Current();
            string userId = currentUser.Id;

            var newPlayList = await spotify.Playlists.Create(userId, new PlaylistCreateRequest(playlist.Name)
            {
                Description = playlist.Description,
                Public = playlist.IsPublic
            });

            return Ok(newPlayList.Name);
        }
    }
}
