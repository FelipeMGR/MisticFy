using MisticFy.Domain.Repositories.Playlists;
using MisticFy.Domain.Repositories.Search;

namespace MisticFy.Domain.Repositories
{
    public interface IUnityOfWork
    {
        IPlaylistRepository PlaylistRepository { get; }
        ISearchRepository SearchRepository { get; }
    }
}
