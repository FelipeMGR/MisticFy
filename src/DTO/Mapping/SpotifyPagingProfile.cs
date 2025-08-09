using AutoMapper;
using MisticFy.src.DTO.DTO;
using MisticFy.src.DTO.DTOs;
using SpotifyAPI.Web;

namespace MisticFy.src.DTO.Mapping
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
