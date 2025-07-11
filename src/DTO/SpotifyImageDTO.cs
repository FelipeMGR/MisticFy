using System;
using System.ComponentModel.DataAnnotations;

namespace MisticFy.DTO;
public class SpotifyImageDTO
{
  [Key]
  public string Url { get; set; }
}
