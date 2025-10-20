using MisticFy.Domain.Models;

namespace MisticFy.Domain.Services;

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
