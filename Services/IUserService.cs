using Microsoft.AspNetCore.Mvc;
using MisticFy.Models;

namespace MisticFy.Services;

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

  Task<ActionResult<Users>> VerifyUser(string userId);  
}
