using System;
using Microsoft.EntityFrameworkCore;
using MisticFy.Models;

namespace MisticFy.Context
{
  public class AppDbContext : DbContext
  {
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Users>? Users { get; set; }
    public DbSet<Playlist> Playlists { get; set; }
    public DbSet<Music> Musics { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);
    }
  }
}
