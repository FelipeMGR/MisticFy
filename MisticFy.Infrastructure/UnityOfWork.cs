using MisticFy.Domain.Repositories;
using MisticFy.Domain.Repositories.Playlists;
using MisticFy.Domain.Repositories.Search;
using MisticFy.Infrastructure.DataAcess.Context;
using MisticFy.Infrastructure.DataAcess.Repositories;

namespace MisticFy.Infrastructure
{
    internal class UnityOfWork : IUnityOfWork
    {

        private IPlaylistRepository? _playlistRepository;
        private ISearchRepository? _searchRepository;
        private readonly AppDbContext? _context;

        public IPlaylistRepository PlaylistRepository => _playlistRepository ??= new PlaylistRepository(_context);

        public ISearchRepository SearchRepository => _searchRepository ??= new SearchRepository(_context);
    }
}
