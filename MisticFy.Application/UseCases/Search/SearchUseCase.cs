using MisticFy.Domain.DTO.SearchMap;
using MisticFy.Domain.Repositories;
using SpotifyAPI.Web;

namespace MisticFy.Application.UseCases.Search
{
    public class SearchUseCase(IUnityOfWork uof)
    {
        private readonly IUnityOfWork _uof = uof;

        public async Task<SpotifySearchResultTrackDTO> SearchResultTrackDTOAsync(string query, SearchRequest.Types types = SearchRequest.Types.Track, int limit = 10)
        {
            return await _uof.SearchRepository.SearchTrackAsync(query, types, limit);
        }

        public async Task<SpotifySearchResultAlbumDTO> SearchResultAlbumDTOAsync(string query, SearchRequest.Types types = SearchRequest.Types.Album, int limit = 10)
        {
            return await _uof.SearchRepository.SearchAlbumAsync(query, types, limit);
        }

        public async Task<SpotifySearchResultArtistDTO> SearchResultArtistDTOAsync(string query, SearchRequest.Types types = SearchRequest.Types.Artist, int limit = 10)
        {
            return await _uof.SearchRepository.SearchArtistAsync(query, types, limit);
        }

        public async Task<SpotifySearchResultPlaylistDTO> SearchResultPlaylistDTOAsync(string query, SearchRequest.Types types = SearchRequest.Types.Playlist, int limit = 10)
        {
            return await _uof.SearchRepository.SearchPlaylistAsync(query, types, limit);
        }
    }
}
