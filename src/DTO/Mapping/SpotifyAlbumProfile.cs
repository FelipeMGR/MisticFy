using AutoMapper;
using MisticFy.src.DTO.DTO;
using SpotifyAPI.Web;

namespace MisticFy.src.DTO.Mapping
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
