using System;
using Microsoft.EntityFrameworkCore;
using MisticFy.Context;
using MisticFy.Models;

namespace MisticFy.Services;

public class UserService(AppDbContext db) : IUserService
{
  private readonly AppDbContext _db = db;

  public async Task<Users> FindOrCreateUserAsync(
    string spotifyUserId,
    string Name,
    string email,
    string accessToken,
    string refreshToken,
    int expiresIn
)
  {
    var user = await _db.Users.FirstOrDefaultAsync(u => u.SpotifyUserId == spotifyUserId);

    if (user == null)
    {
      user = new Users
      {
        SpotifyUserId = spotifyUserId,
        Name = Name,
        Email = email,
        AccessToken = accessToken,
        RefreshToken = refreshToken,
        TokenExpiresAt = DateTime.UtcNow.AddHours(expiresIn)
      };
      _db.Users.Add(user);
    }
    else
    {
      user.AccessToken = accessToken;
      user.RefreshToken = refreshToken;
      user.TokenExpiresAt = DateTime.UtcNow.AddSeconds(expiresIn);
    }

    await _db.SaveChangesAsync();
    return user;
  }

  public async Task<Users> GetUserByIdAsync(int userId)
  {
    return await _db.Users.FirstOrDefaultAsync(x => x.Id == userId);
  }
}
