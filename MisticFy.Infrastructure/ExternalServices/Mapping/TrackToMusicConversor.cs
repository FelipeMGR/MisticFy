using AutoMapper;
using MisticFy.Infrastructure.ExternalServices.Spotify.SpotifyDTO;
using SpotifyAPI.Web;

namespace MisticFy.Infrastructure.ExternalServices.Mapping;

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
