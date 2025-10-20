using AutoMapper;
using MisticFy.Infrastructure.ExternalServices.Spotify.SpotifyDTO;
using SpotifyAPI.Web;

namespace MisticFy.Infrastructure.ExternalServices.Mapping
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
