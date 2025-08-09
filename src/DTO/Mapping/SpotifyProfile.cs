using AutoMapper;
using MisticFy.src.DTO.DTO;
using MisticFy.src.DTO.DTOs;
using MisticFy.src.DTO.DTOs.SearchDTOs;
using SpotifyAPI.Web;

namespace MisticFy.src.DTO.Mapping;

public class SpotifyProfile : Profile
{
    public SpotifyProfile()
    {
        CreateMap<Image, SpotifyImageDTO>()
            .ForMember(dest => dest.Url, opt => opt.MapFrom(src => src.Url));

        CreateMap<PublicUser, SpotifyUserDTO>();

        CreateMap<FullPlaylist, SpotifyPlaylistDetailsDTO>()
            .ForMember(track => track.Musics, t => t.MapFrom(m => m.Tracks.Items));

        CreateMap<SpotifySearchResultGenericDTO, SpotifySearchResultTrackDTO>().ForMember(m => m.Tracks, p => p.MapFrom(o => o.Tracks.Items));
    }

}
