using AutoMapper;
using MisticFy.src.DTO.DTOs;
using SpotifyAPI.Web;

namespace MisticFy.src.DTO.Mapping;

public class TrackToMusicConversor : ITypeConverter<PlaylistTrack<IPlayableItem>, SpotifyMusicDTO>

{
    public SpotifyMusicDTO Convert(PlaylistTrack<IPlayableItem> source, SpotifyMusicDTO destination, ResolutionContext context)
    {
        if (source.Track is FullTrack fullTrack)
        {
            return context.Mapper.Map<SpotifyMusicDTO>(fullTrack);
        }
        return null;
    }

}
