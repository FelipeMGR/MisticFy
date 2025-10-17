using AutoMapper;
using MisticFy.API.src.DTO.SpotifyDTO;
using SpotifyAPI.Web;

namespace MisticFy.API.src.DTO.Mapping
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
