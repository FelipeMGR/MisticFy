using AutoMapper;
using MisticFy.Services;
using MisticFy.src.DTO;
using SpotifyAPI.Web;

namespace MisticFy.src.Services;

public class SpotifyService(IMapper _mapper) : ISpotifyService
{
    public async Task<SpotifySearchResultDTO> SearchAsync(string accessToken, string query, SearchRequest.Types types, int limit)
    {
        SpotifyClient spotify = GetSpotifyClient(accessToken);

        SearchRequest searchRequest = new(types, query)
        {
            Limit = limit
        };

        SearchResponse searchResult = await spotify.Search.Item(searchRequest);

        SpotifySearchResultDTO result = new()
        {
            Tracks = _mapper.Map<SpotifyPagingDTO<MusicDTO>>(searchResult.Tracks),
            Artists = _mapper.Map<SpotifyPagingDTO<SpotifyArtistDTO>>(searchResult.Artists),
            Playlists = _mapper.Map<SpotifyPagingDTO<SpotifyPlaylistDTO>>(searchResult.Playlists),
            Albums = _mapper.Map<SpotifyPagingDTO<SpotifyAlbumDTO>>(searchResult.Albums)
        };

        return result;
    }

    public SpotifyClient GetSpotifyClient(string token)
    {
        var accessToken = token?.Replace("Bearer ", "", StringComparison.OrdinalIgnoreCase).Trim();

        var config = SpotifyClientConfig.CreateDefault().WithToken(accessToken);
        var spotify = new SpotifyClient(config);

        return spotify;
    }
}
