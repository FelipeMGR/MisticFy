using AutoMapper;
using MisticFy.DTO;
using SpotifyAPI.Web;

namespace MisticFy.Services;

public class SpotifyService(IMapper _mapper) : ISpotifyService
{

    public async Task<SpotifySearchResultDTO> SearchAsync(string accessToken, string query, SearchRequest.Types types, int limit)
    {
        var spotify = GetSpotifyClient(accessToken);

        var searchRequest = new SearchRequest(types, query)
        {
          Limit = limit
        };

        var searchResult = await spotify.Search.Item(searchRequest);

        var result = new SpotifySearchResultDTO
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
        var acessToken = token.Replace("Bearer", " ");

        var config = SpotifyClientConfig.CreateDefault().WithToken(acessToken);
        var spotify = new SpotifyClient(config);

        return spotify;
    }
}
