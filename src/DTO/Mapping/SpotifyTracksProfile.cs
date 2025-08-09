using AutoMapper;
using MisticFy.src.DTO.DTO;
using MisticFy.src.DTO.DTOs;
using MisticFy.src.DTO.Mapping;
using SpotifyAPI.Web;

public class SpotifyTracksProfile : Profile
{
    public SpotifyTracksProfile()
    {
        CreateMap<FullTrack, SpotifyMusicDTO>()
            .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Name))
            .ForMember(d => d.Artists, opt => opt.MapFrom(s => s.Artists ?? new List<SimpleArtist>()))
            .ForMember(d => d.Album, opt => opt.MapFrom(s => s.Album));

        CreateMap<PlaylistTrack<IPlayableItem>, SpotifyMusicDTO>().ConvertUsing<TrackToMusicConversor>();
    }
}
