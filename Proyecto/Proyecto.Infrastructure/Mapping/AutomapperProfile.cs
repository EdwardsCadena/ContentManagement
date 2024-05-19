using AutoMapper;
using Proyecto.Core.DTOs;
using Proyecto.Core.Entities;

namespace Proyecto.Infrastructure.Mapping
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<User, UserDTOs>();
            CreateMap<UserDTOs, User>();
            CreateMap<Role, RoleDTOs>();
            CreateMap<RoleDTOs, Role>();
            CreateMap<Article, ArticleDTOs>();
            CreateMap<ArticleDTOs, Article>();
            CreateMap<MultimediaFile, MultimediaFileDTOs>();
            CreateMap<MultimediaFileDTOs, MultimediaFile>();
        }
    }
}
