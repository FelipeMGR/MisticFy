using System;

namespace MisticFy.DTO;

public class SpotifyPagingDTO<T>
{
  public List<T> Items { get; set; }
  public int Total { get; set; }
  public int Limit { get; set; }
  public int Offset { get; set; }
  public string Next { get; set; }
  public string Previous { get; set; }
}
