using API.DTOs;
using API.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<AppUser, AppUserDto>()
                .ForMember(dest => dest.PhotoUrl, opt => opt.MapFrom(src => src.Photos.FirstOrDefault(x=> x.isMain).Url));
            CreateMap<PhotoUser, PhotoUserDto>();
            CreateMap<Track, TrackDto>().ReverseMap();
            CreateMap<AppUserUpdateDto, AppUser>();
            CreateMap<RegisterDto, AppUser>();
            //CreateMap<TrackGenre, TrackDto>();

            CreateMap<TrackGenre, TrackGenreDto>().ReverseMap();
            CreateMap<TrackArtist, TrackArtistDto>().ReverseMap();

            CreateMap<Artist, ArtistDto>().ReverseMap();
            CreateMap<Genre, GenreDto>().ReverseMap();
            CreateMap<Album, AlbumDto>().ReverseMap();
            CreateMap<Playlist, PlaylistDto>().ReverseMap();
        }

    }
}
