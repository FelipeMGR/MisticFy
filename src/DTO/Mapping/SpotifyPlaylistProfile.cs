using AutoMapper;
using MisticFy.API.src.DTO.SpotifyDTO;
using SpotifyAPI.Web;

namespace MisticFy.API.src.DTO.Mapping
{
    public class SpotifyPlaylistProfile : Profile
    {
        public SpotifyPlaylistProfile()
        {
            CreateMap<FullPlaylist, SpotifyPlaylistDetailsDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Owner, opt => opt.MapFrom(src => src.Owner.DisplayName));
        }
    }
}
