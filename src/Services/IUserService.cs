using MisticFy.API.src.Models;

namespace MisticFy.API.src.Services;

public interface IUserService
{
    Task<Users> FindOrCreateUserAsync(
          string spotifyUserId,
          string Name,
          string email,
          string accessToken,
          string refreshToken,
          int expiresIn
      );

    Task<Users> GetUserByIdAsync(int userId);
}
