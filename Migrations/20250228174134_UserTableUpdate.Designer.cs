﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MisticFy.Context;

#nullable disable

namespace MisticFy.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20250228174134_UserTableUpdate")]
    partial class UserTableUpdate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("MisticFy.Models.Music", b =>
                {
                    b.Property<int>("MusicId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("MusicId"));

                    b.Property<string>("MusicName")
                        .HasColumnType("longtext");

                    b.Property<int?>("PlaylistId")
                        .HasColumnType("int");

                    b.HasKey("MusicId");

                    b.HasIndex("PlaylistId");

                    b.ToTable("Musics");
                });

            modelBuilder.Entity("MisticFy.Models.Playlist", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.Property<bool?>("IsPublic")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<int?>("UsersId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UsersId");

                    b.ToTable("Playlists");
                });

            modelBuilder.Entity("MisticFy.Models.Users", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("AccessToken")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Email")
                        .HasColumnType("longtext");

                    b.Property<string>("LikedMusics")
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<string>("RefreshToken")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("SpotifyUserId")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("TokenExpiresAt")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("MisticFy.Models.Music", b =>
                {
                    b.HasOne("MisticFy.Models.Playlist", null)
                        .WithMany("Musics")
                        .HasForeignKey("PlaylistId");
                });

            modelBuilder.Entity("MisticFy.Models.Playlist", b =>
                {
                    b.HasOne("MisticFy.Models.Users", null)
                        .WithMany("Playlists")
                        .HasForeignKey("UsersId");
                });

            modelBuilder.Entity("MisticFy.Models.Playlist", b =>
                {
                    b.Navigation("Musics");
                });

            modelBuilder.Entity("MisticFy.Models.Users", b =>
                {
                    b.Navigation("Playlists");
                });
#pragma warning restore 612, 618
        }
    }
}
