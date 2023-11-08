using API.DTO;
using API.DTOs;
using API.DTOs.UpdateDTOs;
using API.Entities;
using API.Extensions;
using AutoMapper;

namespace API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<AppUser, MemberDto>()
                .ForMember(dest => dest.PhotoUrl, opt => opt.MapFrom(src =>
                    src.Photos.FirstOrDefault(x => x.IsMain).Url))
                .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.DateOfBirth.CalculateAge()));

            CreateMap<Photo, PhotoDto>();

            CreateMap<MemberUpsertDto, AppUser>();
            CreateMap<RegisterDto, AppUser>();

            CreateMap<Genre, GenreDto>();
            CreateMap<GenreUpsertDto, Genre>();

            CreateMap<Artist, ArtistDto>()
                .ForMember(dest => dest.Albums, opt => opt.MapFrom(src => src.Albums));
            CreateMap<ArtistUpsertDto, Artist>();


            CreateMap<Album, AlbumDto>()
                .ForMember(dest => dest.Tracks, opt => opt.MapFrom(src => src.Tracks));

            CreateMap<AlbumUpsertDto, Album>();

            CreateMap<Track, TrackDto>();
            CreateMap<TrackUpsertDto, Track>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<Playlist, PlaylistDto>()
                .ForMember(dest => dest.Tracks, opt => opt.MapFrom(src => src.PlaylistTracks.Select(pt => pt.Track)));
            CreateMap<PlaylistUpsertDto, Playlist>();

            CreateMap<Message, MessageDto>()
                .ForMember(dest => dest.SenderPhotoUrl, opt => opt.MapFrom(src =>
                    src.Sender.Photos.FirstOrDefault(x => x.IsMain).Url))
                .ForMember(dest => dest.RecipientPhotoUrl, opt => opt.MapFrom(src =>
                    src.Recipient.Photos.FirstOrDefault(x => x.IsMain).Url));
        }

    }
}
