using System;
using System.Net;
using System.Net.Http.Headers;
using MisticFy.DTO;
using SpotifyAPI.Web;

namespace MisticFy.Services;

public class SpotifyService : ISpotifyService
{
  public async Task<SpotifySearchResultDTO> SearchAsync(
        string accessToken,
        string query,
        SearchRequest.Types types,
        int limit)
  {
    var config = SpotifyClientConfig.CreateDefault().WithToken(accessToken);
    var spotify = new SpotifyClient(config);

    var searchRequest = new SearchRequest(types, query)
    {
      Limit = limit
    };

    var searchResult = await spotify.Search.Item(searchRequest);

    var result = new SpotifySearchResultDTO
    {
      Tracks = new SpotifyPagingDTO<MusicDTO>
      {
        Items = searchResult.Tracks?.Items.Select(track => new MusicDTO
        {
          Id = track.Id,
          Name = track.Name,
          Artists = track.Artists.Select(artist => new SpotifyArtistDTO
          {
            Id = artist.Id,
            Name = artist.Name,
            Uri = artist.Uri
          }).ToList(),
          Album = new SpotifyAlbumDTO
          {
            Id = track.Album.Id,
            Name = track.Album.Name,
            Images = track.Album.Images.Select(image => new SpotifyImageDTO
            {
              Url = image.Url
            }).ToList()
          }
        }).ToList(),
        Total = searchResult.Tracks?.Total ?? 0,
        Limit = searchResult.Tracks?.Limit ?? 0,
        Offset = searchResult.Tracks?.Offset ?? 0,
        Next = searchResult.Tracks?.Next,
        Previous = searchResult.Tracks?.Previous
      }
    };

    return result;
  }
}
