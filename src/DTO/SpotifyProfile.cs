using AutoMapper;
using MisticFy.Services;
using SpotifyAPI.Web;

namespace MisticFy.src.DTO;

public class SpotifyProfile : Profile
{
    public SpotifyProfile()
    {
        // Specific mapping for Paging<TItem, TNextResponse> -> SpotifyPagingDTO<TItem>
        CreateMap<Paging<FullTrack, SearchResponse>, SpotifyPagingDTO<MusicDTO>>()
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items.OfType<FullTrack>()));

        CreateMap<Paging<FullAlbum, SearchResponse>, SpotifyPagingDTO<SpotifyAlbumDTO>>()
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));

        CreateMap<Paging<FullPlaylist, SearchResponse>, SpotifyPagingDTO<SpotifyPlaylistDTO>>()
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));

        CreateMap<Paging<FullArtist, SearchResponse>, SpotifyPagingDTO<SpotifyArtistDTO>>()
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));

        // Individual mapping
        CreateMap<FullTrack, MusicDTO>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Artists, opt => opt.MapFrom(src => src.Artists))
            .ForMember(dest => dest.Album, opt => opt.MapFrom(src => src.Album));

        CreateMap<SimpleArtist, SpotifyArtistDTO>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

        CreateMap<SimpleAlbum, SpotifyAlbumDTO>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.Images));

        CreateMap<FullPlaylist, SpotifyPlaylistDTO>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Owner, opt => opt.MapFrom(src => src.Owner));

        CreateMap<Image, SpotifyImageDTO>()
            .ForMember(dest => dest.Url, opt => opt.MapFrom(src => src.Url));

        CreateMap<PublicUser, SpotifyUserDTO>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.DisplayName))
            .ForMember(dest => dest.Uri, opt => opt.MapFrom(src => src.Uri));

        CreateMap<FullPlaylist, SpotifyPlaylistDetailsDTO>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.Owner, opt => opt.MapFrom(src => src.Owner))
            .ForMember(dest => dest.Musics, opt => opt.MapFrom(src => src.Tracks.Items.Select(t => t.Track)));

        CreateMap<PlaylistTrack<IPlayableItem>, MusicDTO>()
        .ConvertUsing<TrackToMusicConversor>();

    }

}
