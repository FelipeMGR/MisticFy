using System;
using MisticFy.DTO;
using SpotifyAPI.Web;

namespace MisticFy.Services;

public interface ISpotifyService
{
    Task<SpotifySearchResultDTO> SearchAsync(string accessToken, string query, SearchRequest.Types types, int limit);
    SpotifyClient GetSpotifyClient(string token); 
}
