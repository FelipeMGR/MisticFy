using System;
using MisticFy.src.DTO;
using SpotifyAPI.Web;

namespace MisticFy.Services;

public interface ISpotifyService
{
    Task<SpotifySearchResultDTO> SearchAsync(string accessToken, string query, SearchRequest.Types types, int limit);
    SpotifyClient GetSpotifyClient(string token); 
}
