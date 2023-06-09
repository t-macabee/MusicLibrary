﻿// <auto-generated />
using System;
using API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace API.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20230108161334_initial_migration")]
    partial class initial_migration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.14")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("API.Entities.Album", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AlbumLength")
                        .HasColumnType("int");

                    b.Property<string>("AlbumName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Albums");
                });

            modelBuilder.Entity("API.Entities.AlbumTrack", b =>
                {
                    b.Property<int>("AlbumID")
                        .HasColumnType("int");

                    b.Property<int>("TrackID")
                        .HasColumnType("int");

                    b.HasKey("AlbumID", "TrackID");

                    b.HasIndex("TrackID");

                    b.ToTable("AlbumTracks");
                });

            modelBuilder.Entity("API.Entities.AppUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("About")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("LastActive")
                        .HasColumnType("datetime2");

                    b.Property<string>("Location")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("PasswordHash")
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("API.Entities.Artist", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("About")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ArtistName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("Gender")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Artists");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            About = "Fact 1",
                            ArtistName = "Artist 1",
                            DateOfBirth = new DateTime(2023, 1, 8, 17, 13, 33, 560, DateTimeKind.Local).AddTicks(4891),
                            Gender = "M"
                        },
                        new
                        {
                            Id = 2,
                            About = "Fact 2",
                            ArtistName = "Artist 2",
                            DateOfBirth = new DateTime(2023, 1, 8, 17, 13, 33, 567, DateTimeKind.Local).AddTicks(6336),
                            Gender = "F"
                        },
                        new
                        {
                            Id = 3,
                            About = "Fact 3",
                            ArtistName = "Artist 3",
                            DateOfBirth = new DateTime(2023, 1, 8, 17, 13, 33, 567, DateTimeKind.Local).AddTicks(6480),
                            Gender = "M"
                        },
                        new
                        {
                            Id = 4,
                            About = "Fact 4",
                            ArtistName = "Artist 4",
                            DateOfBirth = new DateTime(2023, 1, 8, 17, 13, 33, 567, DateTimeKind.Local).AddTicks(6486),
                            Gender = "F"
                        },
                        new
                        {
                            Id = 5,
                            About = "Fact 5",
                            ArtistName = "Artist 5",
                            DateOfBirth = new DateTime(2023, 1, 8, 17, 13, 33, 567, DateTimeKind.Local).AddTicks(6490),
                            Gender = "F"
                        },
                        new
                        {
                            Id = 6,
                            About = "Fact 6",
                            ArtistName = "Artist 6",
                            DateOfBirth = new DateTime(2023, 1, 8, 17, 13, 33, 567, DateTimeKind.Local).AddTicks(6493),
                            Gender = "M"
                        },
                        new
                        {
                            Id = 7,
                            About = "Fact 7",
                            ArtistName = "Artist 7",
                            DateOfBirth = new DateTime(2023, 1, 8, 17, 13, 33, 567, DateTimeKind.Local).AddTicks(6496),
                            Gender = "M"
                        });
                });

            modelBuilder.Entity("API.Entities.ExceptionHandlingData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Details")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Message")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StatusCode")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("ExceptionData");
                });

            modelBuilder.Entity("API.Entities.Genre", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("GenreName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Genres");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            GenreName = "Genre 1"
                        },
                        new
                        {
                            Id = 2,
                            GenreName = "Genre 2"
                        },
                        new
                        {
                            Id = 3,
                            GenreName = "Genre 3"
                        },
                        new
                        {
                            Id = 4,
                            GenreName = "Genre 4"
                        },
                        new
                        {
                            Id = 5,
                            GenreName = "Genre 5"
                        },
                        new
                        {
                            Id = 6,
                            GenreName = "Genre 6"
                        },
                        new
                        {
                            Id = 7,
                            GenreName = "Genre 7"
                        });
                });

            modelBuilder.Entity("API.Entities.PhotoAlbum", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AlbumId")
                        .HasColumnType("int");

                    b.Property<string>("PublicId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Url")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("isMain")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("AlbumId");

                    b.ToTable("AlbumCovers");
                });

            modelBuilder.Entity("API.Entities.PhotoUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AppUserId")
                        .HasColumnType("int");

                    b.Property<string>("PublicId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Url")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("isMain")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("AppUserId");

                    b.ToTable("UserPhotos");
                });

            modelBuilder.Entity("API.Entities.Playlist", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("PlaylistName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Playlists");
                });

            modelBuilder.Entity("API.Entities.PlaylistTrack", b =>
                {
                    b.Property<int>("PlaylistID")
                        .HasColumnType("int");

                    b.Property<int>("TrackID")
                        .HasColumnType("int");

                    b.HasKey("PlaylistID", "TrackID");

                    b.HasIndex("TrackID");

                    b.ToTable("PlaylistTracks");
                });

            modelBuilder.Entity("API.Entities.Track", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("About")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Length")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TrackName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Tracks");
                });

            modelBuilder.Entity("API.Entities.TrackArtist", b =>
                {
                    b.Property<int>("TrackID")
                        .HasColumnType("int");

                    b.Property<int>("ArtistID")
                        .HasColumnType("int");

                    b.HasKey("TrackID", "ArtistID");

                    b.HasIndex("ArtistID");

                    b.ToTable("TrackArtists");
                });

            modelBuilder.Entity("API.Entities.TrackGenre", b =>
                {
                    b.Property<int>("GenreID")
                        .HasColumnType("int");

                    b.Property<int>("TrackID")
                        .HasColumnType("int");

                    b.HasKey("GenreID", "TrackID");

                    b.HasIndex("TrackID");

                    b.ToTable("TrackGenres");
                });

            modelBuilder.Entity("API.Entities.AlbumTrack", b =>
                {
                    b.HasOne("API.Entities.Album", "Album")
                        .WithMany("AlbumTracks")
                        .HasForeignKey("AlbumID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("API.Entities.Track", "Track")
                        .WithMany()
                        .HasForeignKey("TrackID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Album");

                    b.Navigation("Track");
                });

            modelBuilder.Entity("API.Entities.PhotoAlbum", b =>
                {
                    b.HasOne("API.Entities.Album", null)
                        .WithMany("AlbumPhotos")
                        .HasForeignKey("AlbumId");
                });

            modelBuilder.Entity("API.Entities.PhotoUser", b =>
                {
                    b.HasOne("API.Entities.AppUser", "AppUser")
                        .WithMany("Photos")
                        .HasForeignKey("AppUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AppUser");
                });

            modelBuilder.Entity("API.Entities.PlaylistTrack", b =>
                {
                    b.HasOne("API.Entities.Playlist", "Playlist")
                        .WithMany("PlaylistTracks")
                        .HasForeignKey("PlaylistID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("API.Entities.Track", "Track")
                        .WithMany()
                        .HasForeignKey("TrackID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Playlist");

                    b.Navigation("Track");
                });

            modelBuilder.Entity("API.Entities.TrackArtist", b =>
                {
                    b.HasOne("API.Entities.Artist", "Artist")
                        .WithMany()
                        .HasForeignKey("ArtistID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("API.Entities.Track", "Track")
                        .WithMany("Artists")
                        .HasForeignKey("TrackID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Artist");

                    b.Navigation("Track");
                });

            modelBuilder.Entity("API.Entities.TrackGenre", b =>
                {
                    b.HasOne("API.Entities.Genre", "Genre")
                        .WithMany()
                        .HasForeignKey("GenreID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("API.Entities.Track", "Track")
                        .WithMany("Genres")
                        .HasForeignKey("TrackID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Genre");

                    b.Navigation("Track");
                });

            modelBuilder.Entity("API.Entities.Album", b =>
                {
                    b.Navigation("AlbumPhotos");

                    b.Navigation("AlbumTracks");
                });

            modelBuilder.Entity("API.Entities.AppUser", b =>
                {
                    b.Navigation("Photos");
                });

            modelBuilder.Entity("API.Entities.Playlist", b =>
                {
                    b.Navigation("PlaylistTracks");
                });

            modelBuilder.Entity("API.Entities.Track", b =>
                {
                    b.Navigation("Artists");

                    b.Navigation("Genres");
                });
#pragma warning restore 612, 618
        }
    }
}
