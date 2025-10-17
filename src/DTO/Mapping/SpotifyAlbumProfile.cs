using AutoMapper;
using MisticFy.API.src.DTO.SpotifyDTO;
using SpotifyAPI.Web;

namespace MisticFy.API.src.DTO.Mapping
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
