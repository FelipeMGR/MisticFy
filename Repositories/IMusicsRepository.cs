using System;
using Microsoft.AspNetCore.Mvc;

namespace MisticFy.Repositories;

public interface IMusicsRepository
{
  Task<ActionResult> GetMusicAsync(string musicName);
}
