using AutoMapper;
using MisticFy.src.DTO.DTO;
using SpotifyAPI.Web;

namespace MisticFy.src.DTO.Mapping
{
    public class SpotifyArtistProfile : Profile
    {
        public SpotifyArtistProfile()
        {
            CreateMap<FullArtist, SpotifyArtistDTO>()
               .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
               .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
        }
    }
}
