using System;
using System.Net;
using System.Net.Http.Headers;
using MisticFy.DTO;

namespace MisticFy.Services;

public class SpotifyService : ISpotifyService
{
  public async Task<SpotifySearchResultDTO> SearchAsync(
        string accessToken,
        string query,
        string types,
        int limit)
  {
    using var client = new HttpClient();
    client.DefaultRequestHeaders.Authorization =
        new AuthenticationHeaderValue("Bearer", accessToken);

    var encodedQuery = WebUtility.UrlEncode(query);
    var url = $"https://api.spotify.com/v1/search?q={encodedQuery}&type={types}&limit={limit}";

    var response = await client.GetAsync(url);
    response.EnsureSuccessStatusCode();

    return await response.Content.ReadFromJsonAsync<SpotifySearchResultDTO>();
  }
}
