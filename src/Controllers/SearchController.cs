using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MisticFy.src.DTO.SearchMap;
using MisticFy.src.Repositories;
using SpotifyAPI.Web;

namespace MisticFy.src.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SearchController(ISearchRepository search) : ControllerBase
    {
        [HttpGet("search-track")]
        [Authorize]
        public async Task<ActionResult<SpotifySearchResultTrackDTO>> SearchTrack([FromQuery] string query, int limit = 10)
        {
            try
            {
                return Ok(await search.SearchTrackAsync(query, SearchRequest.Types.Track, limit));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("search-playlist")]
        [Authorize]
        public async Task<ActionResult<SpotifySearchResultPlaylistDTO>> SearchPlaylist([FromQuery] string query, int limit = 10)
        {
            try
            {
                return Ok(await search.SearchPlaylistAsync(query, SearchRequest.Types.Playlist, limit));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("search-artist")]
        [Authorize]
        public async Task<ActionResult<SpotifySearchResultArtistDTO>> SearchArtist([FromQuery] string query, int limit = 10)
        {
            try
            {
                return Ok(await search.SearchArtistAsync(query, SearchRequest.Types.Artist, limit));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("search-album")]
        [Authorize]
        public async Task<ActionResult<SpotifySearchResultAlbumDTO>> SearchAlbum([FromQuery] string query, int limit = 10)
        {
            try
            {
                return Ok(await search.SearchAlbumAsync(query, SearchRequest.Types.Album, limit));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
