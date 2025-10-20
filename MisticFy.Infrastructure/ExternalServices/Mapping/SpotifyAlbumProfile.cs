using AutoMapper;
using MisticFy.Infrastructure.ExternalServices.Spotify.SpotifyDTO;
using SpotifyAPI.Web;

namespace MisticFy.Infrastructure.ExternalServices.Mapping
{
    public class SpotifyAlbumProfile:Profile
    {
        public SpotifyAlbumProfile()
        {
            CreateMap<SimpleAlbum, SpotifyAlbumDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.ReleaseDate, opt => opt.MapFrom(src => src.ReleaseDate));
        }
    }
}
