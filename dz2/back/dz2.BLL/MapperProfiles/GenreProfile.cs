using AutoMapper;
using spr421_spotify_clone.BLL.Dtos.Genre;
using spr421_spotify_clone.DAL.Entities;

namespace spr421_spotify_clone.BLL.MapperProfiles
{
    public class GenreProfile : Profile
    {
        public GenreProfile()
        {
            // CreateGenreDto -> GenreEntity
            CreateMap<CreateGenreDto, GenreEntity>()
                .ForMember(dest => dest.NormalizedName, opt => opt.MapFrom(src => src.Name.ToUpper()));

            // UpdateGenreDto -> GenreEntity
            CreateMap<UpdateGenreDto, GenreEntity>()
                .ForMember(dest => dest.NormalizedName, opt => opt.MapFrom(src => src.Name.ToUpper()));

            // GenreEntity -> GenreDto
            CreateMap<GenreEntity, GenreDto>();
        }
    }
}
