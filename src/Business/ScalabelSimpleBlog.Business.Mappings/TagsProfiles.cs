using System.Linq;
using AutoMapper;
using ScalabelSimpleBlog.Business.Dto.TagControllerDto;
using ScalabelSimpleBlog.Entities;

namespace ScalabelSimpleBlog.Business.Mappings
{
    public class TagsProfiles : Profile
    {
        protected override void Configure()
        {
            CreateMap<Tag, TagsListWithCountDto>()
                .ForMember(dest => dest.ArticlesCount, opt => opt.MapFrom(x => x.Articles.Count()));

            CreateMap<Tag, TagByArticleId>();
        }
    }
}
