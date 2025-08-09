using AutoMapper;
using MisticFy.src.DTO.DTO;
using SpotifyAPI.Web;

namespace MisticFy.src.DTO.Mapping
{
    public class SpotifyPagingProfile : Profile
    {
        public SpotifyPagingProfile()
        {
            CreateMap(typeof(Paging<,>), typeof(SpotifyPagingDTO<>));
        }
    }
}
