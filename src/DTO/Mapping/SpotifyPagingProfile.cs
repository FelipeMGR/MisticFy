using AutoMapper;
using MisticFy.API.src.DTO.SpotifyDTO;
using SpotifyAPI.Web;

namespace MisticFy.API.src.DTO.Mapping
{
    public class SpotifyPagingProfile : Profile
    {
        public SpotifyPagingProfile()
        {
            CreateMap(typeof(Paging<FullPlaylist, SearchResponse>), typeof(SpotifyPagingDTO<SpotifyPlaylistDetailsDTO>));
            CreateMap(typeof(Paging<FullTrack, SearchResponse>), typeof(SpotifyPagingDTO<SpotifyMusicDTO>));
        }
    }
}
