using MisticFy.src.DTO.DTO;
using MisticFy.src.DTO.DTOs;
using SpotifyAPI.Web;

namespace MisticFy.src.DTO.SearchMap
{
    public static class SearchMapper
    {
        public static SpotifySearchResultGenericDTO MapToGenericDTO(SearchResponse searchResult)
        {
            return new SpotifySearchResultGenericDTO
            {
                Tracks = MapTracks(searchResult.Tracks),
                Albums = MapAlbums(searchResult.Albums),
                Playlists = MapPlaylists(searchResult.Playlists),
                Artists = MapArtists(searchResult.Artists)
            };
        }

        public static SpotifySearchResultAlbumDTO MapToAlbumDTO(SearchResponse searchResult)
        {
            return new SpotifySearchResultAlbumDTO
            {
                Albums = MapAlbums(searchResult.Albums)
            };
        }

        public static SpotifySearchResultArtistDTO MapToArtistDTO(SearchResponse searchResult)
        {
            return new SpotifySearchResultArtistDTO
            {
                Artists = MapArtists(searchResult.Artists)
            };
        }

        public static SpotifySearchResultPlaylistDTO MapToPlaylistDTO(SearchResponse searchResult)
        {
            return new SpotifySearchResultPlaylistDTO
            {
                Playlists = MapPlaylists(searchResult.Playlists)
            };
        }

        public static SpotifySearchResultTrackDTO MapToTrackDTO(SearchResponse searchResult)
        {
            return new SpotifySearchResultTrackDTO
            {
                Tracks = MapTracks(searchResult.Tracks)
            };
        }

        private static List<SpotifyMusicDTO> MapTracks(Paging<FullTrack, SearchResponse> tracks)
        {
            if (tracks?.Items == null) return [];
            return tracks.Items.Select(track => new SpotifyMusicDTO
            {
                Id = track.Id,
                Name = track.Name,
                Artists = track.Artists?.Select(a => new SpotifyArtistDTO
                {
                    Id = a.Id,
                    Name = a.Name
                }).ToList(),
                Album = track.Album != null ? new SpotifyAlbumDTO
                {
                    Id = track.Album.Id,
                    Name = track.Album.Name,
                    ReleaseDate = track.Album.ReleaseDate
                } : null,
                Uri = track.Uri
            }).ToList();
        }

        private static List<SpotifyAlbumDTO> MapAlbums(Paging<SimpleAlbum, SearchResponse> albums)
        {
            if (albums?.Items == null) return [];
            List<SpotifyAlbumDTO> albumCollection = new();

            foreach (var item in albums.Items)
            {
                albumCollection.Add(new SpotifyAlbumDTO
                {
                    Id = item.Id,
                    Name = item.Name,
                    ReleaseDate = item.ReleaseDate
                });
            }
            return albumCollection;
        }

        private static List<SpotifyPlaylistDetailsDTO> MapPlaylists(Paging<FullPlaylist, SearchResponse> playlists)
        {
            if (playlists?.Items == null) return [];

            List<SpotifyPlaylistDetailsDTO> playlistCollection = new();

            foreach (var item in playlists.Items)
            {
                playlistCollection.Add(new SpotifyPlaylistDetailsDTO
                {
                    Id = item.Id,
                    Name = item.Name,
                    Description = item.Description,
                    Musics = new List<SpotifyMusicDTO>
                    {
                        new SpotifyMusicDTO
                        {
                            Id = item.Id,
                            Name = item.Name,
                        }
                    },
                    Owner = item.Owner
                });
            }

            return playlistCollection;
        }

        private static List<SpotifyArtistDTO> MapArtists(Paging<FullArtist, SearchResponse> artists)
        {
            if (artists?.Items == null) return [];
            return artists.Items.Select(artist => new SpotifyArtistDTO
            {
                Id = artist.Id,
                Name = artist.Name
            }).ToList();
        }
    }
}
