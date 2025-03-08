using System;
using MisticFy.DTO;

namespace MisticFy.Services;

public interface ISpotifyService
{
  Task<SpotifySearchResultDTO> SearchAsync(string accessToken, string query, string types, int limit);
}
