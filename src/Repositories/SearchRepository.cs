using AutoMapper;
using MisticFy.Services;
using MisticFy.src.DTO.DTO;
using MisticFy.src.DTO.DTOs;
using MisticFy.src.DTO.DTOs.SearchDTOs;
using SpotifyAPI.Web;

namespace MisticFy.src.Repositories
{
    public class SearchRepository(IHttpContextAccessor httpContext, ISpotifyService spotifyService, IMapper _mapper) : ISearchRepository
    {
        public SpotifySearchResultAlbumDTO SearchAlbumAsync(string query, SearchRequest.Types types = SearchRequest.Types.Album, int limit = 10)
        {
            Task<SpotifySearchResultGenericDTO> result = SearchAsync(query, types, limit);
            return _mapper.Map<SpotifySearchResultAlbumDTO>(result);
        }

        public SpotifySearchResultArtistDTO SearchArtistAsync(string query, SearchRequest.Types types = SearchRequest.Types.Artist, int limit = 10)
        {
            Task<SpotifySearchResultGenericDTO> result = SearchAsync(query, types, limit);
            return _mapper.Map<SpotifySearchResultArtistDTO>(result);
        }

        public SpotifySearchResultPlaylistDTO SearchPlaylistAsync(string query, SearchRequest.Types types = SearchRequest.Types.Playlist, int limit = 10)
        {
            Task<SpotifySearchResultGenericDTO> result = SearchAsync(query, types, limit);
            return _mapper.Map<SpotifySearchResultPlaylistDTO>(result);
        }

        public SpotifySearchResultTrackDTO SearchTrackAsync(string query, SearchRequest.Types types = SearchRequest.Types.Track, int limit = 10)
        {
            Task<SpotifySearchResultGenericDTO> result = SearchAsync(query, types, limit);
            return _mapper.Map<SpotifySearchResultTrackDTO>(result);
        }

        private async Task<SpotifySearchResultGenericDTO> SearchAsync(string query, SearchRequest.Types types, int limit = 10)
        {
            string accessToken = httpContext.HttpContext?.Items["SpotifyAccessToken"].ToString();

            SpotifyClient spotify = spotifyService.GetSpotifyClient(accessToken);

            SearchRequest searchRequest = new(types, query)
            {
                Limit = limit
            };

            SearchResponse searchResult = await spotify.Search.Item(searchRequest);

            SpotifySearchResultGenericDTO generic = new()
            {
                Tracks = _mapper.Map<SpotifyMusicDTO>(searchResult.Tracks),
                Albums = _mapper.Map<SpotifyAlbumDTO>(searchResult.Albums),
                Playlists = _mapper.Map<SpotifyPlaylistDetailsDTO>(searchResult.Playlists),
                Artists = _mapper.Map<SpotifyArtistDTO>(searchResult.Artists)
            };

            return generic;
        }

    }
}
