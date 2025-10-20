using AutoMapper;
using MisticFy.Infrastructure.ExternalServices.Spotify.SpotifyDTO;
using SpotifyAPI.Web;

namespace MisticFy.Infrastructure.ExternalServices.Mapping
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
