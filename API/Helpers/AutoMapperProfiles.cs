using API.DTO;
using API.DTOs;
using API.DTOs.UpdateDTOs;
using API.Entities;
using API.Extensions;
using AutoMapper;
using AutoMapper.QueryableExtensions;


namespace API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<RegisterDto, AppUser>();

            CreateMap<MemberUpsertDto, AppUser>();

            CreateMap<AppUser, MemberDto>()
                .ForMember(dest => dest.PhotoUrl, opt => opt.MapFrom(src =>
                    src.Photos.FirstOrDefault(x => x.IsMain).Url))
                .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.DateOfBirth.CalculateAge()));

            CreateMap<AppUser, LikeDto>();

            CreateMap<Artist, ArtistDto>()
                .ForMember(dest => dest.PhotoUrl, opt => opt.MapFrom(src =>
                    src.Photos.FirstOrDefault(x => x.IsMain).Url))
                .ForMember(dest => dest.Albums, opt => opt.MapFrom(src => src.Albums))
                .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre));

            CreateMap<ArtistUpsertDto, Artist>();

            CreateMap<Photo, PhotoDto>();
            CreateMap<ArtistPhoto, PhotoDto>();
                        
            CreateMap<Genre, GenreDto>();
                        
            CreateMap<Album, AlbumDto>()
                .ForMember(dest => dest.ArtistName, opt => opt.MapFrom(src => src.Artist.ArtistName))
                .ForMember(dest => dest.Tracks, opt => opt.MapFrom(src => src.Tracks));

            CreateMap<AlbumUpsertDto, Album>()
            .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<Track, TrackDto>()
                .ForMember(dest => dest.AlbumName, opt => opt.MapFrom(src => src.Album.AlbumName))
                .ForMember(dest => dest.ArtistName, opt => opt.MapFrom(src => src.Album.Artist.ArtistName));

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
