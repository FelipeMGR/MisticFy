using System;
using AutoMapper;
using MisticFy.DTO;
using SpotifyAPI.Web;

namespace MisticFy.Services;

public class TrackToMusicConversor : ITypeConverter<PlaylistTrack<IPlayableItem>, MusicDTO>

{
  public MusicDTO Convert(
      PlaylistTrack<IPlayableItem> source,
      MusicDTO destination,
      ResolutionContext context)
  {
    if (source.Track is FullTrack fullTrack)
    {
      return context.Mapper.Map<MusicDTO>(fullTrack);
    }
    return null;
  }

}
